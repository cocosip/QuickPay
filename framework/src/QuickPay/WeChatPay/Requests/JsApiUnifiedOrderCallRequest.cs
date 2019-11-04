using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Utility;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>JsApi下单唤起支付
    /// </summary>
    public class JsApiUnifiedOrderCallRequest : BasePayRequest<JsApiUnifiedOrderCallResponse>
    {
        /// <summary>微信支付管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.WeChatPay;

        /// <summary>签名字段名称
        /// </summary>
        public override string SignFieldName => WeChatPaySettings.JsApiPaySignFieldName;

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.JsApi;

        /// <summary>签名类型名称
        /// </summary>
        public override string SignTypeName { get; set; }

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

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            var weChatPayConfig = (WeChatPayConfig)config;
            var weChatPayApp = (WeChatPayApp)app;

            AppId = weChatPayApp.AppId;
            SignType = weChatPayConfig.SignType;
            NonceStr = WeChatPayUtil.GenerateNonceStr();
            Timestamp = WeChatPayUtil.GenerateTimeStamp();

            if (SignTypeName.IsNullOrWhiteSpace())
            {
                //签名类型
                SignTypeName = weChatPayConfig.SignType;
            }
        }

        /// <summary>Ctor
        /// </summary>
        public JsApiUnifiedOrderCallRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="prepayId">预订单Id</param>
        public JsApiUnifiedOrderCallRequest(string prepayId)
        {
            //Package为预订单信息
            Package = $"prepay_id={prepayId}";
        }
    }
}
