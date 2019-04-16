using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>条码支付BizContent
    /// </summary>
    public class BarcodeTradeBizContentPayRequest : BaseBizContentRequest
    {
        /// <summary>商户网站唯一订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>支付场景
        /// </summary>
        [PayElement("scene")]
        public string Scene { get; set; }

        /// <summary>支付授权码，25~30开头的长度为16~24位的数字，实际字符串长度以开发者获取的付款码长度为准
        /// </summary>
        [PayElement("auth_code")]
        public string AuthCode { get; set; }

        /// <summary>订单标题
        /// </summary>
        [PayElement("subject")]
        public string Subject { get; set; }

        /// <summary>订单描述
        /// </summary>
        [PayElement("body", false)]
        public string Body { get; set; }

        /// <summary>订单总金额，单位为元，精确到小数点后两位
        /// 如果同时传入【可打折金额】和【不可打折金额】，该参数可以不用传入； 
        /// 如果同时传入了【可打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【可打折金额】+【不可打折金额】
        /// </summary>
        [PayElement("total_amount", false)]
        public decimal TotalAmount { get; set; }



        /*****************************************/
        /// <summary>销售产品码
        /// </summary>
        [PayElement("product_code", false)]
        public string ProductCode { get; set; } = AlipaySettings.ProductCode.FaceToFace;

        /// <summary>买家的支付宝用户id，如果为空，会从传入了码值信息中获取买家ID
        /// </summary>
        [PayElement("buyer_id", false)]
        public string BuyerId { get; set; }

        /// <summary>如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        [PayElement("seller_id", false)]
        public string SellerId { get; set; }

        /// <summary>参与优惠计算的金额，单位为元，精确到小数点后两位
        /// </summary>
        [PayElement("discountable_amount", false)]
        public decimal DiscountableAmount { get; set; }

        /// <summary>订单包含的商品列表信息，Json格式，其它说明详见商品明细说明
        /// </summary>
        [PayElement("goods_detail", false)]
        public string GoodsDetail { get; set; }

        /// <summary>商户操作员编号
        /// </summary>
        [PayElement("operator_id", false)]
        public string OperatorId { get; set; }

        /// <summary>商户门店编号
        /// </summary>
        [PayElement("store_id", false)]
        public string StoreId { get; set; }

        /// <summary>商户机具终端编号
        /// </summary>
        [PayElement("terminal_id", false)]
        public string TerminalId { get; set; }

        /// <summary>业务扩展参数
        /// </summary>
        [PayElement("extend_params", false)]
        public string ExtendParams { get; set; }

        /// <summary>该笔订单允许的最晚付款时间，逾期将关闭交易
        /// </summary>
        [PayElement("timeout_express", false)]
        public string TimeoutExpress { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BarcodeTradeBizContentPayRequest()
        {

        }
    }
}
