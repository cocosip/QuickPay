using QuickPay.Infrastructure.RequestData;
using QuickPay.Assist;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信支付相关辅助功能
    /// </summary>
    public interface IWechatPayAssistService
    {
        /// <summary>通知签名验证
        /// </summary>
        Task<bool> VerifySign(PayData payData);

        /// <summary>支付成功
        /// </summary>
        Task PaySuccess(PayData payData, Action<PayData, Payment> action = null);

    }
}
