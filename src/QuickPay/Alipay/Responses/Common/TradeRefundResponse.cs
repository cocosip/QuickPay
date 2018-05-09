using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>交易退款
    /// </summary>
    public class TradeRefundResponse : AlipayCommonResponse
    {
        /// <summary>支付宝交易号
        /// </summary>
        [PayElement("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>创建交易传入的商户订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>本笔退款对应的退款请求号
        /// </summary>
        [PayElement("out_request_no", false)]
        public string OutRequestNo { get; set; }

        /// <summary>发起退款时，传入的退款原因
        /// </summary>
        [PayElement("refund_reason", false)]
        public string RefundReason { get; set; }

        /// <summary>该笔退款所对应的交易的订单金额
        /// </summary>
        [PayElement("total_amount", false)]
        public decimal TotalAmount { get; set; }

        /// <summary>本次退款请求，对应的退款金额
        /// </summary>
        [PayElement("refund_amount", false)]
        public decimal RefundAmount { get; set; }


        public TradeRefundResponse()
        {

        }
    }
}
