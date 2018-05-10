using DotCommon.Utility;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.PayAux;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Apps;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>支付信息存储
    /// </summary>
    public class PaymentStoreMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IPaymentStore _paymentStore;
        public PaymentStoreMiddleware(QuickPayExecuteDelegate next, IPaymentStore paymentStore)
        {
            _next = next;
            _paymentStore = paymentStore;
        }

        public async Task Invoke(ExecuteContext context)
        {


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
                TransactionId = ""
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
            }
            else
            {
                payment.PayPlatId = (int)PayPlat.WechatPay;
                payment.AppId = ((WechatPayApp)context.App).AppId;

                //微信从Dictionary字典中获取
                payment.OutTradeNo = context.RequestPayData.GetValue(x => x.Key.ToLower() == "out_trade_no").ToString();
            }
            return payment;
        }

        private bool ShouldStore()
        {
            return false;
            // var paymentTypies = new List<Type>();
        }

    }
}
