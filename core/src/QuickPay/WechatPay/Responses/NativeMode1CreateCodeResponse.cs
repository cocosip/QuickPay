using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>微信扫码支付(Native)模式1,生成二维码
    /// </summary>
    public class NativeMode1CreateCodeResponse : WechatPayTradeResponse
    {
        /// <summary>公众账号ID
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("time_stamp")]
        public string Timestamp { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>商户定义的商品id 或者订单号
        /// </summary>
        [PayElement("product_id")]
        public string ProductId { get; set; }

        /// <summary>签名字段的值
        /// </summary>
        [PayElement("sign")]
        public string Sign { get; set; }

        /// <summary>Ctor
        /// </summary>
        public NativeMode1CreateCodeResponse()
        {

        }

        /// <summary>转换二维码字符串
        /// </summary>
        public string ToCodeUrl()
        {
            return $"weixin://wxpay/bizpayurl?appid={AppId}&mch_id={MchId}&nonce_str={NonceStr}&product_id={ProductId}&time_stamp={Timestamp}&sign={Sign}";
        }
    }
}
