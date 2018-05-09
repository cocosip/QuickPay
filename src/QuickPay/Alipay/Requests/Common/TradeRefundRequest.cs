using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易退款接口
    /// </summary>
    public class TradeRefundRequest : BaseAlipayRequest<TradeRefundResponse>
    {
        public override string Method => "alipay.trade.refund";

        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        public TradeRefundRequest()
        {

        }

        public TradeRefundRequest(TradeRefundBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }

    }
}
