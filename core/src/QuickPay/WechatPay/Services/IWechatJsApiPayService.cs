using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信JsApi支付
    /// </summary>
    public interface IWechatJsApiPayService : IWechatPayService
    {
        /// <summary>JsApi支付统一下单
        /// </summary>
        Task<JsApiUnifiedOrderCallResponse> UnifiedOrder(JsApiUnifiedOrderInput input);

        /// <summary>获取JsSdk config配置
        /// </summary>
        Task<JsSdkConfigResponse> GetJsSdkConfig(string currentUrl);
    }
}
