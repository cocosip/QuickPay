using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>刷卡支付
    /// </summary>
    public interface IWeChatMicroPayService : IWeChatPayService
    {
        /// <summary>刷卡支付提交订单
        /// </summary>
        Task<MicropayUnifiedOrderResponse> UnifiedOrder(MicropayUnifiedOrderInput input);
    }
}
