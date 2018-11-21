using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Services
{
    /// <summary>通用交易接口
    /// </summary>
    public interface IAlipayTradeCommonService : IAlipayService
    {
        /// <summary>支付宝订单查询
        /// </summary>
        Task<TradeQueryResponse> Query(TradeQueryInput input);

        /// <summary>支付订单关闭
        /// </summary>
        Task<TradeCloseResponse> Close(TradeCloseInput input);

        /// <summary>支付退款
        /// </summary>
        Task<TradeRefundResponse> Refund(TradeRefundInput input);

        /// <summary>退款查询
        /// </summary>
        Task<TradeRefundQueryResponse> RefundQuery(TradeRefundQueryInput input);

        /// <summary>查询对账单下载地址
        /// </summary>
        Task<TradeBillDownloadUrlResponse> BillDownloadUrl(TradeBillDownloadUrlInput input);
    }
}
