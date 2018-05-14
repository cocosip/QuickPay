﻿using DotCommon.Runtime;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.RequestData;
using QuickPay.PayAux;
using QuickPay.PayAux.Store;
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
        public WechatPayAssistService(IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider, IPaymentStore paymentStore) : base(wechatPayAppOverrideScopeProvider)
        {
            _paymentStore = paymentStore;
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
                Logger.Error(WechatPayUtil.ParseLog($"微信支付回调签名验证出现异常:{ex.Message}"));
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
            var payment = await _paymentStore.GetAsync((int)PayPlat.WechatPay, App.AppId, payData.GetWechatOutTradeNo());
            if (payment == null)
            {
                throw new QuickPayException($"支付不存在");
            }
            //微信支付订单号
            string transactionId = payData.GetTransactionId();
            try
            {
                //支付状态验证
                if (payment.PayStatusId != (int)PayStatus.Pending && payment.PayStatusId != (int)PayStatus.Processing)
                {
                    throw new QuickPayException($"该笔订单已在本系统中操作过");
                }
                //金额
                if (payment.Amount != payData.GetTotalFeeYuan())
                {
                    throw new QuickPayException(101, $"订单金额不正确,系统存储的金额为:{payment.Amount},回调金额为:{payData.GetTotalFeeYuan()}");
                }
                //业务执行
                if (action != null)
                {
                    action.Invoke(payData, payment);
                }
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
                Logger.Error(WechatPayUtil.ParseLog($"微信支付回调操作出现异常:{ex.Message}"));
                throw;
            }
        }

    }
}