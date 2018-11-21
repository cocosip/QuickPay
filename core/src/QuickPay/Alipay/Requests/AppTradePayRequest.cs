using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>支付宝App支付
    /// </summary>
    public class AppTradePayRequest : BaseAlipayRequest<AppTradePayResponse>
    {
        public override string Method => "alipay.trade.app.pay";

        public override string TradeTypeName => AlipaySettings.TradeType.App;

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。建议商户使用https
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        public AppTradePayRequest()
        {

        }

        public AppTradePayRequest(AppTradeBizContentPayRequest bizContentRequest, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
            NotifyUrl = notifyUrl;
        }


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
