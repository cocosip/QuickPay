using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>微信JsApi支付 JsSDK config签名
    /// </summary>
    public class JsSdkConfigResponse : WechatPayTradeResponse
    {
        /// <summary>应用Id
        /// </summary>
        [PayElement("appId")]
        public string AppId { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("noncestr")]
        public string NonceStr { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>签名字段的值
        /// </summary>
        public string Sign { get; set; }

        /// <summary>Ctor
        /// </summary>
        public JsSdkConfigResponse()
        {

        }
    }
}
