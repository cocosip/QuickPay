using DotCommon.Threading;
using QuickPay.Alipay;
using QuickPay.Assist;
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
        public override async Task<string> InvokeAsync(string notifyBody)
        {
            var payData = PayDataHelper.FromJson(notifyBody);
            var appId = PayDataHelper.GetAlipayAppId(payData);
            using (AlipayAssistService.Use(appId))
            {
                await AlipayAssistService.PaySuccess(payData, payment => AsyncHelper.RunSync(() =>
                {
                    return PaySuccess(payment);
                }));
                //支付成功
                return PaySuccessResponse();
            }
        }

        /// <summary>支付成功返回
        /// </summary>
        protected virtual string PaySuccessResponse()
        {
            return AlipaySettings.NotifyReturn.Success;
        }

        /// <summary>支付失败返回
        /// </summary>
        protected virtual string PayFailResponse()
        {
            return AlipaySettings.NotifyReturn.Fail;
        }

        /// <summary>支付成功的相关业务
        /// </summary>
        public abstract Task PaySuccess(Payment payment);

    }
}