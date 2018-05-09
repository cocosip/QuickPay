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

        //微信默认用MD5
        public override string SignTypeName => WechatPaySettings.SignType.Md5;

        public abstract string RequestUrl { get; }

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

        public virtual void SetNecessary(WechatPayConfig config, WechatPayApp app)
        {
            AppId = app.AppId;
            MchId = app.MchId;
            NonceStr = WechatPayUtil.GenerateNonceStr();
        }
    }
}
