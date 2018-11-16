using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>H5提交订单
    /// </summary>
    public class H5UnifiedOrderRequest : BaseWechatPayRequest<H5UnifiedOrderResponse>
    {
        //public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string TradeTypeName => WechatPaySettings.TradeType.H5;

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
        public string TradeType { get; set; } = WechatPaySettings.TradeType.H5;

        /// <summary>场景信息
        /// </summary>
        [PayElement("scene_info")]
        public string SceneInfo { get; set; }

        public H5UnifiedOrderRequest()
        {

        }

        public H5UnifiedOrderRequest(string body, string outTradeNo, int totalFee, string spbillCreateIp, string sceneInfo, string notifyUrl)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIp = spbillCreateIp;
            SceneInfo = sceneInfo;
            NotifyUrl = notifyUrl;
        }


    }
}
