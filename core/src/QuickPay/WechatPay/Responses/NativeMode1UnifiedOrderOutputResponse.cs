using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WeChatPay.Responses
{
    /// <summary>Native扫码支付模式1统一下单(返回PrepayId给微信服务器)
    /// </summary>
    public class NativeMode1UnifiedOrderOutputResponse : WeChatPayTradeResponse
    {

        /// <summary>SUCCESS/FAIL,此字段是通信标识
        /// </summary>
        [PayElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>返回信息，如非空，为错误原因
        /// </summary>
        [PayElement("return_msg")]
        public string ReturnMsg { get; set; }

        /// <summary>SUCCESS/FAIL
        /// </summary>
        [PayElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>错误信息
        /// </summary>
        [PayElement("err_code_des")]
        public string ErrCodeDes { get; set; }


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

        /// <summary>Ctor
        /// </summary>
        public NativeMode1UnifiedOrderOutputResponse()
        {

        }
    }
}
