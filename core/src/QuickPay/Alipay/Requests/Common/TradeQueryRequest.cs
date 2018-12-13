using QuickPay.Alipay.Responses;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易查询接口
    /// </summary>
    public class TradeQueryRequest : BaseAlipayRequest<TradeQueryResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.query";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeQuery;

        /// <summary>Ctor
        /// </summary>
        public TradeQueryRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        public TradeQueryRequest(TradeQueryBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
