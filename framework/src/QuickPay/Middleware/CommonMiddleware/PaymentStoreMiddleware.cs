﻿using AspectCore.Extensions.Reflection;
using DotCommon.Serializing;
using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Utility;
using QuickPay.Assist;
using QuickPay.Assist.Store;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Utility;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>支付信息存储
    /// </summary>
    public class PaymentStoreMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IPaymentStore _paymentStore;
        private readonly IRequestTypeFinder _requestTypeFinder;
        private readonly AlipayPayDataHelper _alipayPayDataHelper;
        private readonly WeChatPayDataHelper _weChatPayDataHelper;
        private readonly IJsonSerializer _jsonSerializer;
        /// <summary>Ctor
        /// </summary>
        public PaymentStoreMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, IPaymentStore paymentStore, IRequestTypeFinder requestTypeFinder, AlipayPayDataHelper alipayPayDataHelper, WeChatPayDataHelper weChatPayDataHelper, IJsonSerializer jsonSerializer) : base(provider)
        {
            _next = next;
            _paymentStore = paymentStore;
            _requestTypeFinder = requestTypeFinder;
            _alipayPayDataHelper = alipayPayDataHelper;
            _weChatPayDataHelper = weChatPayDataHelper;
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                //判断是否需要调用Store存储支付信息
                if (ShouldStore(context.Request.GetType()))
                {
                    var payment = PreparePayment(context);
                    await _paymentStore.CreateOrUpdateAsync(payment);
                }
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new PaymentStoreError($"存储支付信息发生错误,{ex.Message}"));
                return;
            }

            Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            await _next.Invoke(context);
        }


        private Payment PreparePayment(ExecuteContext context)
        {
            var payment = new Payment()
            {
                UniqueId = context.Request.UniqueId ?? ObjectId.GenerateNewStringId(),
                TradeType = context.Request.TradeTypeName,
                BusinessCode = context.Request.BusinessCode,
                PayStatusId = (int)PayStatus.Pending,
                TransactionId = "",
            };

            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                payment.PayPlatId = (int)PayPlat.Alipay;
                payment.AppId = ((AlipayApp)context.App).AppId;

                //支付宝是获取bizContentRequest中的数据
                var property = context.Request.GetType().GetProperty("BizContentRequest");
                var reflector = property.GetReflector();
                var bizContentRequest = reflector.GetValue(context.Request);
                var payData = RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest);
                payment.OutTradeNo = _alipayPayDataHelper.GetAlipayOutTradeNo(payData);
                //支付宝支付金额
                payment.Amount = _alipayPayDataHelper.GetTotalAmount(payData);

            }
            else
            {
                payment.PayPlatId = (int)PayPlat.WeChatPay;
                payment.AppId = ((WeChatPayApp)context.App).AppId;
                //交易号,本系统唯一
                payment.OutTradeNo = _weChatPayDataHelper.GetOutTradeNo(context.RequestPayData);
                //支付金额,以元为单位,微信是以分为单位,需要进行转换
                payment.Amount = _weChatPayDataHelper.GetTotalFeeYuan(context.RequestPayData);
            }
            if (context.RequestPayData != null)
            {
                var values = context.RequestPayData.GetValues();
                payment.PayObject = _jsonSerializer.Serialize(values);
            }
            else
            {
                Logger.LogInformation(context.Request.GetLogFormat($"因Context中的RequestPayData为NULL,Payment.PayObject将为NULL"));
            }

            return payment;
        }


        private bool ShouldStore(Type requestType)
        {
            var paymentTypies = _requestTypeFinder.FindPaymentStoreTypes();
            if (paymentTypies.Contains(requestType))
            {
                return true;
            }
            return false;
        }

    }
}
