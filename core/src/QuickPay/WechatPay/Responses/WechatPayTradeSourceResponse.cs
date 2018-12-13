using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>包含数据源的微信支付Response
    /// </summary>
    public class WechatPayTradeSourceResponse : PayResponse
    {
        /// <summary>PayData
        /// </summary>
        public PayData PayData { get; set; }
    }
}
