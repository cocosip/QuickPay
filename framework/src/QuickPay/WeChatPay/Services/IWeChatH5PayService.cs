using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信H5支付
    /// </summary>
    public interface IWeChatH5PayService : IWeChatPayService
    {
        /// <summary>H5支付统一下单,返回跳转的url地址
        /// </summary>
         Task<string> UnifiedOrder(H5UnifiedOrderInput input);
    }
}
