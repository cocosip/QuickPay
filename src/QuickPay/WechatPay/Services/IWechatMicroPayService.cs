using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>刷卡支付
    /// </summary>
    public interface IWechatMicroPayService : IWechatPayService
    {
        /// <summary>刷卡支付提交订单
        /// </summary>
        Task<MicropayUnifiedOrderResponse> UnifiedOrder(MicropayUnifiedOrderInput input);
    }
}
