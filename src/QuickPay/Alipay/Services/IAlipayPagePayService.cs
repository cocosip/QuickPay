using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>PC网站支付
    /// </summary>
    public interface IAlipayPagePayService : IAlipayService
    {
        /// <summary>PC网站支付生成订单
        /// </summary>
        Task<PageTradePayResponse> TradePay(PageTradePayInput input);

        /// <summary>PC网站支付生成订单Get请求字符串
        /// </summary>
        Task<string> TradePayStringResponse(PageTradePayInput input);

    }
}
