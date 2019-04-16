using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Util;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信支付泛型抽象基类
    /// </summary>
    public abstract class BaseWeChatPayRequest<T> : BasePayRequest<T> where T : PayResponse
    {
        /// <summary>微信支付管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.WeChatPay;

        /// <summary>签名字段名称
        /// </summary>
        public override string SignFieldName => WeChatPaySettings.DefaultSignFieldName;

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => "";

        /// <summary>签名类型名称
        /// </summary>
        public override string SignTypeName { get; set; } = "";

        /// <summary>AppId
        /// </summary>
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

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            var wechatPayConfig = (WeChatPayConfig)config;
            var wechatPayApp = (WeChatPayApp)app;
            AppId = wechatPayApp.AppId;
            MchId = wechatPayApp.MchId;
            NonceStr = WeChatPayUtil.GenerateNonceStr();

            if (SignTypeName.IsNullOrWhiteSpace())
            {
                //签名类型
                SignTypeName = wechatPayConfig.SignType;
            }
        }
    }
}
