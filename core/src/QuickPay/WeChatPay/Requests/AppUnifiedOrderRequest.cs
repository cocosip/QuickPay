using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>微信App下单
    /// </summary>
    public class AppUnifiedOrderRequest : BaseWeChatPayRequest<AppUnifiedOrderResponse>
    {
        //public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.TradeType.App;

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
        public string TradeType { get; set; } = WeChatPaySettings.TradeType.App;

        /// <summary>Ctor
        /// </summary>
        public AppUnifiedOrderRequest()
        {

        }


        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">订单总金额,单位为分</param>
        public AppUnifiedOrderRequest(string body, string outTradeNo, int totalFee)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">订单总金额,单位为分</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public AppUnifiedOrderRequest(string body, string outTradeNo, int totalFee, string notifyUrl)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            NotifyUrl = notifyUrl;
        }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            var wechatPayConfig = (WeChatPayConfig)config;
            SpbillCreateIp = wechatPayConfig.LocalAddress;
            if (NotifyUrl.IsNullOrWhiteSpace())
            {
                NotifyUrl = wechatPayConfig.GetDefaultNotifyUrl();
            }
        }


        /********************非必须参数********************/
        /// <summary>自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来
        /// </summary>
        [PayElement("detail", false)]
        public string Detail { get; set; }

        /// <summary>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        [PayElement("attach", false)]
        public string Attach { get; set; }

        /// <summary>符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        [PayElement("fee_type", false)]
        public string FeeType { get; set; }

        /// <summary>订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        [PayElement("time_start", false)]
        public string TimeStart { get; set; }

        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010 
        /// 注意：最短失效时间间隔必须大于5分钟
        /// </summary>
        [PayElement("time_expire", false)]
        public string TimeExpire { get; set; }

        /// <summary>商品标记，使用代金券或立减优惠功能时需要的参数
        /// </summary>
        [PayElement("goods_tag", false)]
        public string GoodsTag { get; set; }

        /// <summary>商品ID,trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义
        /// </summary>
        [PayElement("product_id", false)]
        public string ProductId { get; set; }

        /// <summary>上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        [PayElement("limit_pay", false)]
        public string LimitPay { get; set; }


    }
}
