using QuickPay.Assist;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay;
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
        public override async Task<string> InvokeAsync(string notifyBody)
        {
            var payData = PayDataHelper.FromXml(notifyBody);
            var wechatPayApp = GetApp(payData);
            using(WechatPayAssistService.Use(wechatPayApp))
            {
                await WechatPayAssistService.PaySuccess(payData, async payment => await PaySuccess(payment));
                //支付成功返回
                return PaySuccessResponse();
            }
        }

        /// <summary>支付成功返回
        /// </summary>
        protected virtual string PaySuccessResponse()
        {
            var payData = new PayData();
            payData.SetValue("return_code", WechatPaySettings.NotifyReturn.Success);
            payData.SetValue("return_msg", WechatPaySettings.ReturnMsg.Ok);
            return PayDataHelper.ToXml(payData);
        }

        /// <summary>支付失败返回
        /// </summary>
        protected virtual string PayFailResponse()
        {
            var payData = new PayData();
            payData.SetValue("return_code", WechatPaySettings.NotifyReturn.Fail);
            payData.SetValue("return_msg", "ERROR");
            return PayDataHelper.ToXml(payData);
        }


        /// <summary>支付成功的相关业务
        /// </summary>
        public abstract Task PaySuccess(Payment payment);

    }
}