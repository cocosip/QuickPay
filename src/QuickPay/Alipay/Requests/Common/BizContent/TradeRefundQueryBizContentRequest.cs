using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易退款查询BizContent
    /// </summary>
    public class TradeRefundQueryBizContentRequest : BaseBizContentRequest
    {
        /// <summary>支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [PayElement("trade_no", false)]
        public string TradeNo { get; set; }

        /// <summary>订单支付时传入的商户订单号,和支付宝交易号不能同时为空。
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>请求退款接口时，传入的退款请求号，如果在退款请求时未传入，则该值为创建交易时的外部交易号
        /// </summary>
        [PayElement("out_request_no")]
        public string OutRequestNo { get; set; }

        public TradeRefundQueryBizContentRequest()
        {

        }

        public TradeRefundQueryBizContentRequest(string outTradeNo, string outRequestNo)
        {
            OutTradeNo = outTradeNo;
            OutRequestNo = outRequestNo;
        }
    }
}
