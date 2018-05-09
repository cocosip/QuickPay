using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    public interface IWechatAppPayService : IWechatPayService
    {
        /// <summary>App支付统一下单
        /// </summary>
        Task<AppUnifiedOrderCallResponse> UnifiedOrder(AppUnifiedOrderInput input);


    }
}
