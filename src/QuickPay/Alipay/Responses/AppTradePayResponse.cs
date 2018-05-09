using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>支付宝App支付
    /// </summary>
    public class AppTradePayResponse : AlipayTradeResponse
    {
        /// <summary>异步通知地址
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        public AppTradePayResponse()
        {

        }
    }
}
