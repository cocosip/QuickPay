using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Util;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信JsApi支付 JsSDK config签名
    /// </summary>
    public class JsSdkConfigRequest : BaseWechatPayRequest<JsSdkConfigResponse>
    {
        /// <summary>签名字段名称
        /// </summary>
        public override string SignFieldName { get; } = WeChatPaySettings.JsSdkConfigSignFieldName;

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.JsApi;

        /// <summary>签名类型名称
        /// </summary>
        public override string SignTypeName => WeChatPaySettings.SignType.Sha1;

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

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WeChatPayUtil.GenerateTimeStamp();
        }

        /// <summary>Ctor
        /// </summary>
        public JsSdkConfigRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="jsApiTicket">JsApiTicket</param>
        /// <param name="currentUrl">当前网址的Url</param>
        public JsSdkConfigRequest(string jsApiTicket, string currentUrl)
        {
            JsApiTicket = jsApiTicket;
            CurrentUrl = currentUrl;
        }
    }
}
