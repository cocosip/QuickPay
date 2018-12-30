using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信小程序支付
    /// </summary>
    public interface IWeChatMiniProgramPayService : IWeChatPayService
    {
        /// <summary>小程序支付统一下单
        /// </summary>
        Task<MiniProgramUnifiedOrderCallResponse> UnifiedOrder(MiniProgramUnifiedOrderInput input);
    }
}
