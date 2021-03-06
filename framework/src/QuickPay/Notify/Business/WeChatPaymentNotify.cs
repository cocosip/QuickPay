using DotCommon.Threading;
using QuickPay.Assist;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>微信支付支付结果通知
    /// </summary>
    public abstract class WeChatPaymentNotify : WeChatPayNotify
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatPaymentNotify(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>执行业务
        /// </summary>
        public override async Task<string> InvokeAsync(string notifyBody)
        {
            var payData = PayDataHelper.FromXml(notifyBody);
            var appId = PayDataHelper.GetWeChatAppId(payData);

            using (WeChatPayAssistService.Use(appId))
            {
                await WeChatPayAssistService.PaySuccess(payData, payment => AsyncHelper.RunSync(() =>
                {
                    return PaySuccess(payment);
                }));
                //支付成功返回
                return PaySuccessResponse();
            }
        }

        /// <summary>支付成功返回
        /// </summary>
        protected virtual string PaySuccessResponse()
        {
            var payData = new PayData();
            payData.SetValue("return_code", WeChatPaySettings.NotifyReturn.Success);
            payData.SetValue("return_msg", WeChatPaySettings.ReturnMsg.Ok);
            return PayDataHelper.ToXml(payData);
        }

        /// <summary>支付失败返回
        /// </summary>
        protected virtual string PayFailResponse()
        {
            var payData = new PayData();
            payData.SetValue("return_code", WeChatPaySettings.NotifyReturn.Fail);
            payData.SetValue("return_msg", "ERROR");
            return PayDataHelper.ToXml(payData);
        }


        /// <summary>支付成功的相关业务
        /// </summary>
        public abstract Task PaySuccess(Payment payment);

    }
}