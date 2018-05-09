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



    }
}
