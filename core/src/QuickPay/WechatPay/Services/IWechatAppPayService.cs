using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信App支付
    /// </summary>
    public interface IWechatAppPayService : IWechatPayService
    {
        /// <summary>App支付统一下单
        /// </summary>
        Task<AppUnifiedOrderCallResponse> UnifiedOrder(AppUnifiedOrderInput input);


    }
}
