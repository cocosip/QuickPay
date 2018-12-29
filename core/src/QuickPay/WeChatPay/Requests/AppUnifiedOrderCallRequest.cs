using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Util;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信支付App下单唤起支付
    /// </summary>
    public class AppUnifiedOrderCallRequest : BaseWechatPayRequest<AppUnifiedOrderCallResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.App;

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

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WeChatPayUtil.GenerateTimeStamp();
        }

        /// <summary>Ctor
        /// </summary>
        public AppUnifiedOrderCallRequest()
        {

        }

        /// <summary>
        /// </summary>
        public AppUnifiedOrderCallRequest(string prepayId)
        {
            PrepayId = prepayId;
        }


    }
}
