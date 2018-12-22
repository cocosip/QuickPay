using QuickPay.Assist;
using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>微信支付支付结果通知
    /// </summary>
    public abstract class WechatPaymentNotify : WechatPayNotify
    {
        /// <summary>Ctor
        /// </summary>
        public WechatPaymentNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>执行业务
        /// </summary>
        public override async Task InvokeAsync(string notifyBody)
        {
            var payData = PayDataHelper.FromXml(notifyBody);
            var wechatPayApp = GetApp(payData);
            using(WechatPayAssistService.Use(wechatPayApp))
            {
                await WechatPayAssistService.PaySuccess(payData, async payment => await PaySuccess(payment));
            }
        }

        /// <summary>支付成功的相关业务
        /// </summary>
        public abstract Task PaySuccess(Payment payment);

    }
}
