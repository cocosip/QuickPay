using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>Native扫码支付模式1统一下单(返回PrepayId给微信服务器)
    /// </summary>
    public class NativeMode1UnifiedOrderRequest : BaseWechatPayRequest<NativeMode1UnifiedOrderResponse>
    {
        public override string RequestUrl => "";

        public override string TradeTypeName => WechatPaySettings.TradeType.Native;

        /// <summary>商品简单描述，该字段请按照规范传递
        /// </summary>
        [PayElement("body")]
        public string Body { get; set; }

        /// <summary>商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额，单位为分
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        [PayElement("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }

        /// <summary>异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数
        /// </summary>
        [PayElement("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>交易类型,取值如下：JSAPI，NATIVE，APP等
        /// </summary>
        [PayElement("trade_type")]
        public string TradeType { get; set; } = WechatPaySettings.TradeType.Native;

        /// <summary>签名类型
        /// </summary>
        [PayElement("sign_type")]
        public string SignType { get; set; }
        public override void SetNecessary(WechatPayConfig config, WechatPayApp app)
        {
            base.SetNecessary(config, app);
            SignType = config.SignType;
        }

        public NativeMode1UnifiedOrderRequest()
        {

        }
        public NativeMode1UnifiedOrderRequest(string body, string outTradeNo, int totalFee, string spbillCreateIp, string notifyUrl)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIp = spbillCreateIp;
            NotifyUrl = notifyUrl;
        }

    }
}
