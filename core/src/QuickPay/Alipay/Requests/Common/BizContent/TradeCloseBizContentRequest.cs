using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易关闭BizContent
    /// </summary>
    public class TradeCloseBizContentRequest : BaseBizContentRequest
    {
        /// <summary>订单支付时传入的商户订单号,和支付宝交易号不能同时为空。 trade_no,out_trade_no如果同时存在优先取trade_no
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>该交易在支付宝系统中的交易流水号。最短 16 位，最长 64 位。和out_trade_no不能同时为空，如果同时传了 out_trade_no和 trade_no，则以 trade_no为准。
        /// </summary>
        [PayElement("trade_no", false)]
        public string TradeNo { get; set; }

        /// <summary>卖家端自定义的的操作员 ID
        /// </summary>
        [PayElement("operator_id", false)]
        public string OperatorId { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeCloseBizContentRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">订单支付时传入的商户订单号</param>
        public TradeCloseBizContentRequest(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
