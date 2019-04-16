using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>条码支付生成预订单
    /// </summary>
    public class QrcodeTradePayRequest : BaseAlipayRequest<QrcodeTradePayResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.precreate";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.TradeType.QrcodePay;

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。建议商户使用https
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public QrcodeTradePayRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public QrcodeTradePayRequest(QrcodeTradeBizContentPayRequest bizContentRequest, string notifyUrl)
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
                NotifyUrl = ((AlipayConfig)config).GetDefaultQrcodeNotifyUrl();
            }
        }
    }
}
