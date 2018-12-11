using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Util;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;

namespace QuickPay.Alipay.Requests
{
    public abstract class BaseAlipayRequest<T> : BasePayRequest<T> where T : BaseAlipayResponse
    {
        public override string Provider => QuickPaySettings.Provider.Alipay;

        public override string SignFieldName => AlipaySettings.DefaultSignFieldName;
        public override string TradeTypeName => "";

        public override string SignTypeName { get; set; }

        /// <summary>AppId
        /// </summary>
        [PayElement("app_id")]
        public string AppId { get; set; }

        /// <summary>请求方法名
        /// </summary>
        [PayElement("method")]
        public abstract string Method { get; }

        /// <summary>仅支持JSON
        /// </summary>
        [PayElement("format")]
        public string Format { get; set; }

        /// <summary>请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        [PayElement("charset")]
        public string Charset { get; set; }

        /// <summary>签名类型
        /// </summary>
        [PayElement("sign_type")]
        public string SignType { get; set; }

        /// <summary>版本
        /// </summary>
        [PayElement("version")]
        public string Version { get; set; }

        /// <summary>版本
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>BizContent
        /// </summary>
        [PayElement("biz_content")]
        public string BizContent { get; set; }

        /// <summary>BizContent请求
        /// </summary>
        public BaseBizContentRequest BizContentRequest { get; set; }

        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            var alipayConfig = (AlipayConfig)config;
            var alipayApp = (AlipayApp)app;

            Format = alipayConfig.Format;
            Version = alipayConfig.Version;
            AppId = alipayApp.AppId;
            Charset = alipayApp.Charset;
            SignType = alipayApp.SignType;
            Timestamp = AlipayUtil.GenerateTimeStamp();
        }
    }
}
