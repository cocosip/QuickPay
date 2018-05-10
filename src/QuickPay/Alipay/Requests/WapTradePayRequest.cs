using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>手机网站支付
    /// </summary>
    public class WapTradePayRequest : BaseAlipayRequest<WapTradePayResponse>
    {
        public override string Method => "alipay.trade.wap.pay";

        public override string TradeTypeName => AlipaySettings.TradeType.Wap;

        /// <summary>HTTP/HTTPS开头字符串,同步结果通知
        /// </summary>
        [PayElement("return_url", false)]
        public string ReturnUrl { get; set; }

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        public WapTradePayRequest()
        {

        }

        public WapTradePayRequest(WapTradeBizContentPayRequest bizContentRequest, string returnUrl, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
            ReturnUrl = returnUrl;
            NotifyUrl = notifyUrl;
        }

        public override void SetNecessary(AlipayConfig config, AlipayApp app)
        {
            base.SetNecessary(config, app);
            if (NotifyUrl.IsNullOrWhiteSpace())
            {
                NotifyUrl = config.GetDefaultNotifyUrl();
            }
        }
    }
}
