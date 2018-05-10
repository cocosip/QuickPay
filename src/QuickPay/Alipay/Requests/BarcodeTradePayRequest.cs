using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>条码支付
    /// </summary>
    public class BarcodeTradePayRequest : BaseAlipayRequest<BarcodeTradePayResponse>
    {
        /// <summary>统一下单
        /// </summary>
        public override string Method => "alipay.trade.pay";
        public override string TradeTypeName => AlipaySettings.TradeType.BarcodePay;

        /// <summary>支付宝服务器主动通知商户服务器里指定的页面http/https路径。建议商户使用https
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        public BarcodeTradePayRequest()
        {

        }

        public BarcodeTradePayRequest(BarcodeTradeBizContentPayRequest bizContentRequest, string notifyUrl)
        {
            BizContentRequest = bizContentRequest;
            NotifyUrl = notifyUrl;
        }

        public override void SetNecessary(AlipayConfig config, AlipayApp app)
        {
            base.SetNecessary(config, app);
            if (NotifyUrl.IsNullOrWhiteSpace())
            {
                NotifyUrl = config.GetDefaultBarcodeNotifyUrl();
            }
        }
    }
}
