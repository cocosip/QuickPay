﻿using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>JsApi下单唤起支付
    /// </summary>
    public class JsApiUnifiedOrderCallRequest : BasePayRequest<JsApiUnifiedOrderCallResponse>
    {
        public override string Provider => QuickPaySettings.Provider.WechatPay;

        public override string SignFieldName => WechatPaySettings.JsApiPaySignFieldName;

        public override string TradeTypeName => WechatPaySettings.TradeType.JsApi;
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


        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            var wechatPayConfig = (WechatPayConfig)config;
            var wechatPayApp = (WechatPayApp)app;

            AppId = wechatPayApp.AppId;
            SignType = wechatPayConfig.SignType;
            NonceStr = WechatPayUtil.GenerateNonceStr();
            Timestamp = WechatPayUtil.GenerateTimeStamp();
        }

        public JsApiUnifiedOrderCallRequest()
        {

        }

        public JsApiUnifiedOrderCallRequest(string prepayId)
        {
            //Package为预订单信息
            Package = $"prepay_id={prepayId}";
        }
    }
}
