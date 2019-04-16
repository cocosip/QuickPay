using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>条码支付
    /// </summary>
    public interface IAlipayBarcodePayService
    {
        /// <summary>条码支付统一下单
        /// </summary>
        Task<BarcodeTradePayResponse> TradePay(BarcodeTradePayInput input);
    }
}
