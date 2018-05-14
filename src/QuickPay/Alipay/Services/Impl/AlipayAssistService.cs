﻿using DotCommon.Runtime;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.RequestData;
using QuickPay.PayAux;
using QuickPay.PayAux.Store;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝辅助服务
    /// </summary>
    public class AlipayAssistService : BaseAlipayService, IAlipayAssistService
    {
        private readonly IPaymentStore _paymentStore;
        public AlipayAssistService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider, IPaymentStore paymentStore) : base(alipayAppOverrideScopeProvider)
        {
            _paymentStore = paymentStore;
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
                Logger.Error(AlipayUtil.ParseLog($"支付回调签名验证出现异常:{ex.Message}"));
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
            var payment = await _paymentStore.GetAsync((int)PayPlat.Alipay, App.AppId, payData.GetAlipayOutTradeNo());
            if (payment == null)
            {
                throw new QuickPayException($"支付不存在");
            }
            //支付宝流水号
            string tradeNo = payData.GetAlipayTradeNo();

            try
            {
                //支付状态验证
                if (payment.PayStatusId != (int)PayStatus.Pending && payment.PayStatusId != (int)PayStatus.Processing)
                {
                    throw new QuickPayException($"该笔订单已在本系统中操作过");
                }
                //支付宝那边的订单状态验证
                //交易成功
                if (payData.GetTradeStatus() != AlipaySettings.TradeStatus.TradeSuccess)
                {
                    throw new QuickPayException(101, $"交易状态不为交易成功:{payData.GetTradeStatus()}");
                }
                //金额
                if (payment.Amount != payData.GetTotalAmount())
                {
                    throw new QuickPayException(101, $"订单金额不正确,系统存储的金额为:{payment.Amount},回调金额为:{payData.GetTotalAmount()}");
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
                Logger.Error(AlipayUtil.ParseLog($"支付回调操作出现异常:{ex.Message}"), ex);
                throw;
            }
        }
    }
}