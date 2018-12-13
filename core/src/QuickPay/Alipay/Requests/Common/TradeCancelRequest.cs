using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{

    /// <summary>交易取消
    /// </summary>
    public class TradeCancelRequest : BaseAlipayRequest<TradeCancelResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.cancel";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeCancel;

        /// <summary>应用认证Token
        /// </summary>
        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeCancelRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        public TradeCancelRequest(TradeCancelBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }

    }
}
