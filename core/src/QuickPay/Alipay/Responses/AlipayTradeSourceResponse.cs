using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>交易返回,通常是没有调用支付宝接口,而是本地拼接将数据传递给支付宝那边的,包含PayData数据源
    /// </summary>
    public abstract class AlipayTradeSourceResponse : BaseAlipayResponse
    {
        /// <summary>AppId
        /// </summary>
        [PayElement("app_id")]
        public string AppId { get; set; }

        /// <summary>Method
        /// </summary>
        [PayElement("method")]
        public string Method { get; set; }

        /// <summary>仅支持JSON
        /// </summary>
        [PayElement("format")]
        public string Format { get; set; }

        /// <summary>请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        [PayElement("charset")]
        public string Charset { get; set; }

        /// <summary>签名类型,商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        [PayElement("sign_type")]
        public string SignType { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>调用的接口版本，固定为：1.0
        /// </summary>
        [PayElement("version")]
        public string Version { get; set; }

        /// <summary>业务请求参数的集合，最大长度不限，除公共参数外所有请求参数都必须放在这个参数中传递，具体参照各产品快速接入文档	
        /// </summary>
        [PayElement("biz_content")]
        public string BizContent { get; set; }

        /// <summary>签名字段名
        /// </summary>
        public string SignField { get; set; }

        /// <summary>数据源
        /// </summary>
        public PayData PayData { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AlipayTradeSourceResponse()
        {

        }
    }
}
