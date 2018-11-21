using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services
{
    /// <summary>微信通用交易接口
    /// </summary>
    public interface IWechatPayTradeCommonService : IWechatPayService
    {
        /// <summary>根据订单交易号查询订单
        /// </summary>
        Task<OrderQueryResponse> OrderQuery(OrderQueryInput input);

        /// <summary>关闭订单
        /// </summary>
        Task<OrderCloseResponse> OrderClose(OrderCloseInput input);

        /// <summary>交易退款
        /// </summary>
        Task<OrderRefundResponse> Refund(OrderRefundInput input);

        /// <summary>退款查询
        /// </summary>
        Task<RefundQueryResponse> RefundQuery(RefundQueryInput input);

        /// <summary>上报
        /// </summary>
        Task<ReportResponse> Report(ReportInput input);
    }
}
