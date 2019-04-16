using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>交易退款接口
    /// </summary>
    public class TradeRefundRequest : BaseAlipayRequest<TradeRefundResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.refund";
        
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeRefund;

        /// <summary>应用认证Token
        /// </summary>
        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeRefundRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        public TradeRefundRequest(TradeRefundBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }

    }
}
