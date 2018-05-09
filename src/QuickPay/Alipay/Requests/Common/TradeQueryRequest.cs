using QuickPay.Alipay.Responses;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易查询接口
    /// </summary>
    public class TradeQueryRequest : BaseAlipayRequest<TradeQueryResponse>
    {
        public override string Method => "alipay.trade.query";

        public TradeQueryRequest()
        {

        }

        public TradeQueryRequest(TradeQueryBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
