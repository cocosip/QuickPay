using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>PC网页支付返回
    /// </summary>
    public class PageTradePayResponse : AlipayTradeSourceResponse
    {
        /// <summary>同步通知地址
        /// </summary>
        [PayElement("return_url")]
        public string ReturnUrl { get; set; }

        /// <summary>异步通知地址
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public PageTradePayResponse()
        {

        }
    }
}
