using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>刷卡支付
    /// </summary>
    public class MicropayUnifiedOrderRequest : BaseWechatPayRequest<MicropayUnifiedOrderResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WechatPaySettings.TradeType.Micropay;

        /// <summary>商品简单描述，该字段请按照规范传递
        /// </summary>
        [PayElement("body")]
        public string Body { get; set; }

        /// <summary>商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额,单位为分
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        [PayElement("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }

        /// <summary>刷卡支付授权码，设备读取用户微信中的条码或者二维码信息
        /// 注：用户刷卡条形码规则：18位纯数字，以10、11、12、13、14、15开头
        /// </summary>
        [PayElement("auth_code")]
        public string AuthCode { get; set; }


        ///// <summary>交易类型,取值如下：JSAPI，NATIVE，APP等
        ///// </summary>
        //[PayElement("trade_type")]
        //public string TradeType { get; set; } = WxpayConsts.WxpayTradeType.App;

        /// <summary>Ctor
        /// </summary>
        public MicropayUnifiedOrderRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">商户系统内部订单号</param>
        /// <param name="authCode">刷卡支付授权码,设备读取用户微信中的条码或者二维码信息</param>
        public MicropayUnifiedOrderRequest(string body, string outTradeNo, int totalFee, string authCode)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;

        }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            SpbillCreateIp = ((WechatPayConfig)config).LocalAddress;
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

        /// <summary>商品标记，使用代金券或立减优惠功能时需要的参数
        /// </summary>
        [PayElement("goods_tag", false)]
        public string GoodsTag { get; set; }

        /// <summary>上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        [PayElement("limit_pay", false)]
        public string LimitPay { get; set; }
    }
}
