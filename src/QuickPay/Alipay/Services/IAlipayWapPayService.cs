using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>支付宝手机网站支付
    /// </summary>
    public interface IAlipayWapPayService : IAlipayService
    {
        /// <summary>支付宝手机网站支付生成支付信息
        /// </summary>
        Task<WapTradePayResponse> TradePay(WapTradePayInput input);
    }
}
