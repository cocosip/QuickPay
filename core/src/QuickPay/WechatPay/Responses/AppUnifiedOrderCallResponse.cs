using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{

    /// <summary>App支付统一支付唤起支付
    /// </summary>
    public class AppUnifiedOrderCallResponse : WechatPayTradeResponse
    {
        /// <summary>微信开放平台审核通过的应用APPID
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("partnerid")]
        public string PartnerId { get; set; }

        /// <summary>微信返回的支付交易会话ID
        /// </summary>
        [PayElement("prepayid")]
        public string PrepayId { get; set; }

        /// <summary>扩展字段,暂填写固定值Sign=WXPay
        /// </summary>
        [PayElement("package")]
        public string Package { get; set; } = "Sign=WXPay";

        /// <summary>随机字符串，不长于32位
        /// </summary>
        [PayElement("noncestr")]
        public string NonceStr { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AppUnifiedOrderCallResponse()
        {

        }
    }
}
