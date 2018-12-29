using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信JsApi支付
    /// </summary>
    public interface IWeChatJsApiPayService : IWeChatPayService
    {
        /// <summary>JsApi支付统一下单
        /// </summary>
        Task<JsApiUnifiedOrderCallResponse> UnifiedOrder(JsApiUnifiedOrderInput input);

        /// <summary>获取JsSdk config配置
        /// </summary>
        Task<JsSdkConfigResponse> GetJsSdkConfig(string currentUrl);
    }
}
