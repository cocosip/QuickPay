using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>jsApi统一下单唤起支付
    /// </summary>
    public class JsApiUnifiedOrderCallResponse : WechatPayTradeResponse
    {
        /// <summary>微信开放平台审核通过的应用APPID
        /// </summary>
        [PayElement("appId")]
        public string AppId { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timeStamp")]
        public string Timestamp { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonceStr")]
        public string NonceStr { get; set; }

        /// <summary>订单详情扩展字符串
        /// </summary>
        [PayElement("package")]
        public string Package { get; set; }

        /// <summary>签名类型
        /// </summary>
        [PayElement("signType")]
        public string SignType { get; set; }


        public JsApiUnifiedOrderCallResponse()
        {

        }
    }
}
