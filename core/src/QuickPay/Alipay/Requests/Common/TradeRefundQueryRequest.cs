using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>退款查询
    /// </summary>
    public class TradeRefundQueryRequest : BaseAlipayRequest<TradeRefundQueryResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.fastpay.refund.query";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeRefundQuery;

        /// <summary>应用认证Token
        /// </summary>
        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeRefundQueryRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        public TradeRefundQueryRequest(TradeRefundQueryBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
