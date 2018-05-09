using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>退款查询
    /// </summary>
    public class TradeRefundQueryRequest : BaseAlipayRequest<TradeRefundQueryResponse>
    {
        public override string Method => "alipay.trade.fastpay.refund.query";

        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        public TradeRefundQueryRequest()
        {

        }

        public TradeRefundQueryRequest(TradeRefundQueryBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
