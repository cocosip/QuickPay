using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    public abstract class BaseWechatPayRequest<T> : BasePayRequest<T> where T : PayResponse
    {
        public override string Provider => QuickPaySettings.Provider.WechatPay;

        public override string SignFieldName => WechatPaySettings.DefaultSignFieldName;
        public override string TradeTypeName => "";
        public override string SignTypeName { get; set; } = "";

        //public virtual string RequestUrl { get; }

        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>商户系统内部的订单号,32个字符内、可包含字母
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            var wechatPayConfig = (WechatPayConfig)config;
            var wechatPayApp = (WechatPayApp)app;
            AppId = wechatPayApp.AppId;
            MchId = wechatPayApp.MchId;
            NonceStr = WechatPayUtil.GenerateNonceStr();

            if (SignTypeName.IsNullOrWhiteSpace())
            {
                //签名类型
                SignTypeName = wechatPayConfig.SignType;
            }
        }
    }
}
