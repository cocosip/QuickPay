using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>App统一下单请求实体
    /// </summary>
    [AutoMapTo(typeof(AppUnifiedOrderRequest))]
    public class AppUnifiedOrderInput : UniqueIdDto
    {
        /// <summary>商品简单描述，该字段请按照规范传递
        /// </summary>
        public string Body { get; set; }

        /// <summary>商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数
        /// </summary>
        public string NotifyUrl { get; set; }

        public AppUnifiedOrderInput()
        {

        }

        public AppUnifiedOrderInput(string body, string outTradeNo, int totalFee)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
        }
        public AppUnifiedOrderInput(string body, string outTradeNo, int totalFee, string notifyUrl)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            NotifyUrl = notifyUrl;
        }

        /********************非必须参数********************/
        /// <summary>自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来
        /// </summary>
        public string Detail { get; set; }

        /// <summary>附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        public string Attach { get; set; }

        /// <summary>符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string FeeType { get; set; }

        /// <summary>订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        public string TimeStart { get; set; }

        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010 
        /// 注意：最短失效时间间隔必须大于5分钟
        /// </summary>
        public string TimeExpire { get; set; }

        /// <summary>商品标记，使用代金券或立减优惠功能时需要的参数
        /// </summary>
        public string GoodsTag { get; set; }

        /// <summary>商品ID,trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public string LimitPay { get; set; }
    }
}
