using QuickPay.Assist;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>支付宝支付结果通知
    /// </summary>
    public abstract class AlipayPaymentNotify : AlipayNotify
    {
        /// <summary>Ctor
        /// </summary>
        public AlipayPaymentNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>执行业务
        /// </summary>
        public override async Task InvokeAsync(string notifyBody)
        {
            var payData = PayDataHelper.FromJson(notifyBody);
            var alipayApp = GetApp(payData);
            using(AlipayAssistService.Use(alipayApp))
            {
                await AlipayAssistService.PaySuccess(payData, async payment => await PaySuccess(payment));
            }
        }

        /// <summary>支付成功的相关业务
        /// </summary>
        public abstract Task PaySuccess(Payment payment);

    }
}
