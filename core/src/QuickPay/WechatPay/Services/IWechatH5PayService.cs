using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    public interface IWechatH5PayService : IWechatPayService
    {
        /// <summary>H5支付统一下单,返回跳转的url地址
        /// </summary>
         Task<string> UnifiedOrder(H5UnifiedOrderInput input);
    }
}
