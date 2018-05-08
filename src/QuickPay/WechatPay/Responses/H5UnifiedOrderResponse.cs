using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    public class H5UnifiedOrderResponse : BaseWechatPayResponse
    {
        /********************以下字段在return_code为SUCCESS的时候有返回********************/

        /// <summary>微信分配的公众账号ID
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>随机字符串，不长于32位
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>签名
        /// </summary>
        [PayElement("sign")]
        public string Sign { get; set; }

        /// <summary>调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP
        /// </summary>
        [PayElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        [PayElement("prepay_id")]
        public string PrepayId { get; set; }

        /// <summary>支付跳转链接
        /// </summary>
        [PayElement("mweb_url")]
        public string MWebUrl { get; set; }

        public H5UnifiedOrderResponse()
        {

        }
    }
}
