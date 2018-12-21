using DotCommon.Threading;
using Microsoft.Extensions.Logging;
using QuickPay.Assist;
using QuickPay.Assist.Store;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信支付相关辅助
    /// </summary>
    public class WechatPayAssistService : BaseWechatPayService, IWechatPayAssistService
    {
        private readonly IPaymentStore _paymentStore;
        private readonly WechatPayDataHelper _wechatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WechatPayAssistService(IServiceProvider provider, IPaymentStore paymentStore, WechatPayDataHelper wechatPayDataHelper) : base(provider)
        {
            _paymentStore = paymentStore;
            _wechatPayDataHelper = wechatPayDataHelper;
        }

        /// <summary>通知签名验证
        /// </summary>
        public async Task<bool> VerifySign(PayData payData)
        {
            try
            {
                return await Task.FromResult(WechatPayUtil.VerifySign(payData, App));
            }
            catch (Exception ex)
            {
                Logger.LogError(WechatPayUtil.ParseLog($"微信支付回调签名验证出现异常:{ex.Message}"));
                return false;
            }
        }

        /// <summary>支付成功
        /// </summary>
        public async Task PaySuccess(PayData payData, Action<Payment> action = null)
        {
            //没有值
            if (!payData.HasValue())
            {
                throw new QuickPayException("微信支付回调出现异常.");
            }
            var payment = await _paymentStore.GetAsync((int)PayPlat.WechatPay, App.AppId, _wechatPayDataHelper.GetOutTradeNo(payData));
            if (payment == null)
            {
                Logger.LogError(WechatPayUtil.ParseLog($"支付信息不存在,AppId:{App.AppId}"));
                throw new QuickPayException($"支付信息不存在");
            }
            //微信支付订单号
            string transactionId = _wechatPayDataHelper.GetTransactionId(payData);
            //payData.GetTransactionId();
            try
            {
                //支付状态验证
                if (payment.PayStatusId != (int)PayStatus.Pending && payment.PayStatusId != (int)PayStatus.Processing)
                {
                    throw new QuickPayException($"该笔订单已在本系统中操作过");
                }

                //交易成功
                if (_wechatPayDataHelper.GetResultCode(payData) != WechatPaySettings.ResultCode.Success)
                {
                    throw new QuickPayException(101, $"支付不成功:{_wechatPayDataHelper.GetResultCode(payData)}");
                }

                //金额
                if (payment.Amount != _wechatPayDataHelper.GetTotalFeeYuan(payData))
                {
                    throw new QuickPayException(101, $"订单金额不正确,系统存储的金额为:{payment.Amount},回调金额为:{_wechatPayDataHelper.GetTotalFeeYuan(payData)}");
                }
                //业务执行
                action?.Invoke(payment);

                //支付成功后支付状态改变
                payment.PayStatusId = (int)PayStatus.Success;
                payment.TransactionId = transactionId;
                await _paymentStore.CreateOrUpdateAsync(payment);
            }
            catch (QuickPayException ex)
            {
                if (ex.Code == 101)
                {
                    payment.PayStatusId = (int)PayStatus.Processing;
                    await _paymentStore.CreateOrUpdateAsync(payment);
                }
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(WechatPayUtil.ParseLog($"微信支付回调操作出现异常:{ex.Message}"));
                throw;
            }
        }

    }
}
