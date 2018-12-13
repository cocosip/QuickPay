using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>微信扫码支付(Native)模式1,生成二维码
    /// </summary>
    public class NativeMode1CreateCodeRequest : BaseWechatPayRequest<NativeMode1CreateCodeResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WechatPaySettings.TradeType.Native;

        /// <summary>系统当前时间
        /// </summary>
        [PayElement("time_stamp")]
        public string Timestamp { get; set; }

        /// <summary>商户定义的商品id 或者订单号
        /// </summary>
        [PayElement("product_id")]
        public string ProductId { get; set; }

        /// <summary>Ctor
        /// </summary>
        public NativeMode1CreateCodeRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="productId">商户定义的商品id 或者订单号</param>
        public NativeMode1CreateCodeRequest(string productId)
        {
            ProductId = productId;
        }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            Timestamp = WechatPayUtil.GenerateTimeStamp();
        }
    }
}
