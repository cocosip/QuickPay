using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信小程序支付
    /// </summary>
    public interface IWechatMiniProgramPayService : IWechatPayService
    {
        /// <summary>小程序支付统一下单
        /// </summary>
        Task<MiniProgramUnifiedOrderCallResponse> UnifiedOrder(MiniProgramUnifiedOrderInput input);
    }
}
