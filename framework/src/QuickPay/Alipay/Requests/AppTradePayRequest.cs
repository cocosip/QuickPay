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
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.app.pay";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.TradeType.App;

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。建议商户使用https
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AppTradePayRequest()
        {

        }

        /// <summary>
        /// </summary>
        /// <param name="bizContentRequest">BizContent数据</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public AppTradePayRequest(AppTradeBizContentPayRequest bizContentRequest, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
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
