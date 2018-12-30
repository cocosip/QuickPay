using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信App支付
    /// </summary>
    public interface IWeChatAppPayService : IWeChatPayService
    {
        /// <summary>App支付统一下单
        /// </summary>
        Task<AppUnifiedOrderCallResponse> UnifiedOrder(AppUnifiedOrderInput input);


    }
}
