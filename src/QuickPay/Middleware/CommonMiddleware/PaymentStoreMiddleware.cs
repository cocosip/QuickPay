using DotCommon.Utility;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.PayAux;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Apps;
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
        public PaymentStoreMiddleware(QuickPayExecuteDelegate next, IPaymentStore paymentStore, IRequestTypeFinder requestTypeFinder)
        {
            _next = next;
            _paymentStore = paymentStore;
            _requestTypeFinder = requestTypeFinder;
        }

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
                Logger.Error(context.Request.GetLogFormat($"存储支付信息发生错误,{ex.Message}"));
                SetPipelineError(context, new PaymentStoreError("存储支付信息发生错误"));
                return;
            }

            Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
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
                var bizContentRequest = property.GetValue(context.Request);
                var payData = RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest);
                payment.OutTradeNo = payData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
                //支付金额,以元为单位,微信是以分为单位,需要进行转换
                payment.Amount = Convert.ToDecimal(payData.GetValue(x => x.Key.ToLower() == "total_amount"));

            }
            else
            {
                payment.PayPlatId = (int)PayPlat.WechatPay;
                payment.AppId = ((WechatPayApp)context.App).AppId;

                //交易号,本系统唯一
                payment.OutTradeNo = context.RequestPayData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();

                //支付金额,以元为单位,微信是以分为单位,需要进行转换
                payment.Amount = Convert.ToDecimal(Convert.ToInt32(context.RequestPayData.GetValue(x => x.Key.ToLower() == "total_fee")) / 100.0);
            }
            return payment;
        }


        private bool ShouldStore(Type requestType)
        {
            var paymentTypies = _requestTypeFinder.FindPaymentStoreTypies();
            if (paymentTypies.Contains(requestType))
            {
                return true;
            }
            return false;
        }

    }
}
