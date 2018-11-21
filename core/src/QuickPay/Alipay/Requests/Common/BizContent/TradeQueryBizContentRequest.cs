using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易查询BizContent
    /// </summary>
    public class TradeQueryBizContentRequest : BaseBizContentRequest
    {
        /// <summary>	订单支付时传入的商户订单号,和支付宝交易号不能同时为空
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>支付宝交易号，和商户订单号不能同时为空(特殊可选)
        /// </summary>
        [PayElement("trade_no", false)]
        public string TradeNo { get; set; }

        public TradeQueryBizContentRequest()
        {

        }

        public TradeQueryBizContentRequest(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
