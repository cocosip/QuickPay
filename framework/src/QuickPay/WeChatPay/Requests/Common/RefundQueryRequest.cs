using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>退款查询
    /// </summary>
    public class RefundQueryRequest : BaseWeChatPayRequest<RefundQueryResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.ExtTradeType.OrderRefundQuery;

        /// <summary>微信订单号
        /// </summary>
        [PayElement("transaction_id", false)]
        public string TransactionId { get; set; }

        /// <summary>商户系统内部订单号
        /// </summary>
        [PayElement("out_trade_no", false)]
        public string OutTradeNo { get; set; }

        /// <summary>商户退款单号
        /// </summary>
        [PayElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>微信退款单号
        /// </summary>
        [PayElement("refund_id", false)]
        public string RefundId { get; set; }

        /// <summary>Ctor
        /// </summary>
        public RefundQueryRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outRefundNo">商户系统内部订单号</param>
        public RefundQueryRequest(string outRefundNo)
        {
            OutRefundNo = outRefundNo;
        }
    }
}
