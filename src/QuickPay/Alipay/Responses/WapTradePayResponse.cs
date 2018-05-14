using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>手机网站支付
    /// </summary>
    public class WapTradePayResponse : AlipayTradeSourceResponse
    {
        /// <summary>同步通知地址
        /// </summary>
        [PayElement("return_url")]
        public string ReturnUrl { get; set; }

        /// <summary>异步通知地址
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        public WapTradePayResponse()
        {

        }
    }
}
