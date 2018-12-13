using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易退款BizContent请求
    /// </summary>
    public class TradeRefundBizContentRequest : BaseBizContentRequest
    {
        /// <summary>订单支付时传入的商户订单号,不能和 trade_no同时为空
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [PayElement("trade_no", false)]
        public string TradeNo { get; set; }

        /// <summary>需要退款的金额,该金额不能大于订单金额,单位为元,支持两位小数
        /// </summary>
        [PayElement("refund_amount")]
        public decimal RefundAmount { get; set; }

        /// <summary>退款原因
        /// </summary>
        [PayElement("refund_reason", false)]
        public string RefundReason { get; set; }

        /// <summary>标识一次退款请求，同一笔交易多次退款需要保证唯一，如需部分退款，则此参数必传。
        /// </summary>
        [PayElement("out_request_no", false)]
        public string OutRefundNo { get; set; }

        /// <summary>商户的操作员编号
        /// </summary>
        [PayElement("operator_id", false)]
        public string OperatorId { get; set; }

        /// <summary>商户的门店编号
        /// </summary>
        [PayElement("store_id", false)]
        public string StoreId { get; set; }

        /// <summary>商户的终端编号
        /// </summary>
        [PayElement("terminal_id")]
        public string TerminalId { get; set; }
        
        /// <summary>Ctor
        /// </summary>
        public TradeRefundBizContentRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">订单支付时传入的商户订单号</param>
        /// <param name="refundAmount">需要退款的金额,该金额不能大于订单金额,单位为元,支持两位小数。如:1.00</param>
        public TradeRefundBizContentRequest(string outTradeNo, decimal refundAmount)
        {
            OutTradeNo = outTradeNo;
            RefundAmount = refundAmount;
        }
    }
}
