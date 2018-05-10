using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    public class AppUnifiedOrderCallRequest : BaseWechatPayRequest<AppUnifiedOrderCallResponse>
    {
        public override string RequestUrl => "";
        public override string TradeTypeName => WechatPaySettings.TradeType.App;

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("partnerid")]
        public string PartnerId { get; set; }

        /// <summary>微信返回的支付交易会话ID
        /// </summary>
        [PayElement("prepayid")]
        public string PrepayId { get; set; }

        /// <summary>扩展字段,暂填写固定值Sign=WXPay
        /// </summary>
        [PayElement("package")]
        public string Package { get; set; } = "Sign=WXPay";

        /// <summary>随机字符串，不长于32位
        /// </summary>
        [PayElement("noncestr")]
        public new string NonceStr { get; set; }

        /// <summary>时间戳
        /// </summary>
        [PayElement("timestamp")]
        public string Timestamp { get; set; }

        public override void SetNecessary(WechatPayConfig config, WechatPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WechatPayUtil.GenerateTimeStamp();
        }
        public AppUnifiedOrderCallRequest()
        {

        }

        public AppUnifiedOrderCallRequest(string prepayId)
        {
            PrepayId = prepayId;
        }


    }
}
