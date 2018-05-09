using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>条码支付生成预订单
    /// </summary>
    public class QrcodeTradePayRequest : BaseAlipayRequest<QrcodeTradePayResponse>
    {
        //统一收单线下交易预创建
        public override string Method => "alipay.trade.precreate";

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。建议商户使用https
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }
        public QrcodeTradePayRequest()
        {

        }

        public QrcodeTradePayRequest(QrcodeTradeBizContentPayRequest bizContentRequest, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
            NotifyUrl = notifyUrl;
        }

        public override void SetNecessary(AlipayConfig config, AlipayApp app)
        {
            base.SetNecessary(config, app);
            if (NotifyUrl.IsNullOrWhiteSpace())
            {
                NotifyUrl = config.GetDefaultQrcodeNotifyUrl();
            }
        }
    }
}
