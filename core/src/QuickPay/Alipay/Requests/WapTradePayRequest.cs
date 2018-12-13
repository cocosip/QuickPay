using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>手机网站支付
    /// </summary>
    public class WapTradePayRequest : BaseAlipayRequest<WapTradePayResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.wap.pay";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.TradeType.Wap;

        /// <summary>HTTP/HTTPS开头字符串,同步结果通知
        /// </summary>
        [PayElement("return_url", false)]
        public string ReturnUrl { get; set; }

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public WapTradePayRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        /// <param name="returnUrl">同步结果通知</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public WapTradePayRequest(WapTradeBizContentPayRequest bizContentRequest, string returnUrl, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
            ReturnUrl = returnUrl;
            NotifyUrl = notifyUrl;
        }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            if (NotifyUrl.IsNullOrWhiteSpace())
            {
                NotifyUrl = ((AlipayConfig)config).GetDefaultNotifyUrl();
            }
        }
    }
}
