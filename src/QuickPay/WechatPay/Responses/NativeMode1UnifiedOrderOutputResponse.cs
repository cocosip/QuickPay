using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    public class NativeMode1UnifiedOrderOutputResponse : BaseWechatPayResponse
    {

        /// <summary>AppId
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>预支付ID
        /// </summary>
        [PayElement("prepay_id")]
        public string PrepayId { get; set; }


        public NativeMode1UnifiedOrderOutputResponse()
        {

        }
    }
}
