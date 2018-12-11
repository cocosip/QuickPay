using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>微信JsApi支付 JsSDK config签名
    /// </summary>
    public class JsSdkConfigRequest : BaseWechatPayRequest<JsSdkConfigResponse>
    {

        public override string SignFieldName { get; } = WechatPaySettings.JsSdkConfigSignFieldName;

        public override string TradeTypeName => WechatPaySettings.TradeType.JsApi;

        public override string SignTypeName => WechatPaySettings.SignType.Sha1;

        /// <summary>应用Id
        /// </summary>
        [PayElement("appId")]
        public new string AppId { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("noncestr")]
        public new string NonceStr { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>JsApiTicket
        /// </summary>
        [PayElement("jsapi_ticket")]
        public string JsApiTicket { get; set; }

        /// <summary>当前网址的Url
        /// </summary>
        [PayElement("url")]
        public string CurrentUrl { get; set; }

        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WechatPayUtil.GenerateTimeStamp();
        }

        public JsSdkConfigRequest()
        {

        }

        public JsSdkConfigRequest(string jsApiTicket, string currentUrl)
        {
            JsApiTicket = jsApiTicket;
            CurrentUrl = currentUrl;
        }
    }
}
