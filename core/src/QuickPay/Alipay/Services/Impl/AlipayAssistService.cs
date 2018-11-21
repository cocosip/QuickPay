using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Assist;
using QuickPay.Assist.Store;
using System;
using System.Threading.Tasks;
namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝辅助服务
    /// </summary>
    public class AlipayAssistService : BaseAlipayService, IAlipayAssistService
    {
        private readonly IPaymentStore _paymentStore;
        private readonly AlipayPayDataHelper _alipayPayDataHelper;
        public AlipayAssistService(IServiceProvider provider, IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider, IPaymentStore paymentStore) : base(provider, alipayAppOverrideScopeProvider)
        {
            _paymentStore = paymentStore;
            _alipayPayDataHelper = provider.GetService<AlipayPayDataHelper>();
        }

        /// <summary>签名验证
        /// </summary>
        public async Task<bool> VerifySign(PayData payData)
        {
            try
            {
                return await Task.FromResult(AlipaySignature.RSACheckV1(payData.GetValues(), App.PublicKey, App.Charset, App.SignType));
            }
            catch (Exception ex)
            {
                Logger.LogError(AlipayUtil.ParseLog($"支付回调签名验证出现异常:{ex.Message}"));
                return false;
            }
        }


        /// <summary>支付成功
        /// </summary>
        public async Task PaySuccess(PayData payData, Action<PayData, Payment> action = null)
        {
            //签名验证
            if (!(await VerifySign(payData)))
            {
                throw new QuickPayException($"签名不正确");
            }
            var payment = await _paymentStore.GetAsync((int)PayPlat.Alipay, App.AppId, _alipayPayDataHelper.GetAlipayOutTradeNo(payData));
            if (payment == null)
            {
                throw new QuickPayException($"支付不存在");
            }
            //支付宝流水号
            string tradeNo = _alipayPayDataHelper.GetAlipayTradeNo(payData);

            try
            {
                //支付状态验证
                if (payment.PayStatusId != (int)PayStatus.Pending && payment.PayStatusId != (int)PayStatus.Processing)
                {
                    throw new QuickPayException($"该笔订单已在本系统中操作过");
                }
                //支付宝那边的订单状态验证
                //交易成功
                if (_alipayPayDataHelper.GetTradeStatus(payData) != AlipaySettings.TradeStatus.TradeSuccess)
                {
                    throw new QuickPayException(101, $"交易状态不为交易成功:{_alipayPayDataHelper.GetTradeStatus(payData)}");
                }
                //金额
                if (payment.Amount != _alipayPayDataHelper.GetTotalAmount(payData))
                {
                    throw new QuickPayException(101, $"订单金额不正确,系统存储的金额为:{payment.Amount},回调金额为:{_alipayPayDataHelper.GetTotalAmount(payData)}");
                }
                if (action != null)
                {
                    //业务执行
                    action.Invoke(payData, payment);
                }
                //支付成功后支付状态改变
                payment.PayStatusId = (int)PayStatus.Success;
                //支付宝流水号
                payment.TransactionId = tradeNo;
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
                Logger.LogError(AlipayUtil.ParseLog($"支付回调操作出现异常:{ex.Message}"), ex);
                throw;
            }
        }
    }
}
