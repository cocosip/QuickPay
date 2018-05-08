using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>微信扫码支付(Native)模式1,生成二维码
    /// </summary>
    public class NativeMode1CreateCodeRequest : BaseWechatPayRequest<NativeMode1CreateCodeResponse>
    {
        public override string RequestUrl => "";

        /// <summary>系统当前时间
        /// </summary>
        [PayElement("time_stamp")]
        public string Timestamp { get; set; }

        /// <summary>商户定义的商品id 或者订单号
        /// </summary>
        [PayElement("product_id")]
        public string ProductId { get; set; }

        public NativeMode1CreateCodeRequest()
        {

        }

        public NativeMode1CreateCodeRequest(string productId)
        {
            ProductId = productId;
        }

        public override void SetNecessary(WechatPayConfig config, WechatPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WechatPayUtil.GenerateTimeStamp();
        }
    }
}
