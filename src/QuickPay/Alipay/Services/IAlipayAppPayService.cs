using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>App支付
    /// </summary>
    public interface IAlipayAppPayService : IAlipayService
    {
        /// <summary>支付宝支付生成支付信息
        /// </summary>
        Task<AppTradePayResponse> TradePay(AppTradePayInput input);

        /// <summary>支付宝支付生成支付信息返回可以直接提交的字符串
        /// </summary>
        Task<string> TradePayStringResponse(AppTradePayInput input);
    }
}
