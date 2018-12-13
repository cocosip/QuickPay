using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>扫码支付生成预订单BizContent
    /// </summary>
    public class QrcodeTradeBizContentPayRequest : BaseBizContentRequest
    {
        /// <summary>商户网站唯一订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额
        /// </summary>
        [PayElement("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>订单标题
        /// </summary>
        [PayElement("subject")]
        public string Subject { get; set; }

        /// <summary>对交易或商品的描述	
        /// </summary>
        [PayElement("body", false)]
        public string Body { get; set; }


        /// <summary>卖家支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        [PayElement("seller_id", false)]
        public string SellerId { get; set; }

        /// <summary>可打折金额. 参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】
        /// </summary>
        [PayElement("discountable_amount", false)]
        public decimal DiscountableAmount { get; set; }

        /// <summary>订单包含的商品列表信息.Json格式. 其它说明详见：“商品明细说明”
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

        /// <summary>该笔订单允许的最晚付款时间，逾期将关闭交易。
        /// </summary>
        [PayElement("timeout_express", false)]
        public string TimeoutExpress { get; set; }

        /// <summary>Ctor
        /// </summary>
        public QrcodeTradeBizContentPayRequest()
        {

        }
    }
}
