using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Utility;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;

namespace QuickPay.Alipay.Requests
{
    /// <summary>支付宝基础泛型请求
    /// </summary>
    public abstract class BaseAlipayRequest<T> : BasePayRequest<T> where T : BaseAlipayResponse
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.Alipay;

        /// <summary>签名字段名称
        /// </summary>
        public override string SignFieldName => AlipaySettings.DefaultSignFieldName;

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => "";

        /// <summary>签名类型名称
        /// </summary>
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

        /// <summary>设置BizContent
        /// </summary>
        public void SetBizContentRequest(BaseBizContentRequest baseBizContentRequest)
        {
            BizContentRequest = baseBizContentRequest;
        }

        /// <summary>设置BizContent
        /// </summary>
        public void SetBizContentString(string bizContent)
        {
            BizContent = bizContent;
        }

        /// <summary>设置必要参数
        /// </summary>
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

            //扩展数据
            Extras.Add(AlipaySettings.ExtraNames.BizContentRequest, BizContentRequest);
        }
    }
}
