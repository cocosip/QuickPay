using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>PC网站支付BizContent
    /// </summary>
    public class PageTradeBizContentPayRequest: BaseBizContentRequest
    {
        /// <summary>对一笔交易的具体描述信息
        /// </summary>
        [PayElement("body", false)]
        public string Body { get; set; }

        /// <summary>商品的标题/交易标题/订单标题/订单关键字等
        /// </summary>
        [PayElement("subject")]
        public string Subject { get; set; }

        /// <summary>商户网站唯一订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>销售产品码，与支付宝签约的产品码名称
        /// </summary>
        [PayElement("product_code")]
        public string ProductCode { get; set; } = AlipaySettings.ProductCode.Page;

        /// <summary>订单总金额，单位为元，精确到小数点后两位
        /// </summary>
        [PayElement("total_amount")]
        public string TotalAmount { get; set; }



        //////////////////////////////////////////////////////////////////////////


        /// <summary>设置未付款支付宝交易的超时时间，一旦超时，该笔交易就会自动被关闭
        /// 该参数数值不接受小数点， 如 1.5h，可转换为 90m
        /// </summary>
        [PayElement("timeout_express", false)]
        public string TimeoutExpress { get; set; }

        /// <summary>收款支付宝用户ID。
        /// </summary>
        [PayElement("seller_id", false)]
        public string SellerId { get; set; }

        /// <summary>商品类型
        /// </summary>
        [PayElement("goods_type", false)]
        public string GoodType { get; set; }

        /// <summary>公用回传参数，如果请求时传递了该参数，则返回给商户时会回传该参数
        /// </summary>
        [PayElement("passback_params", false)]
        public string PassbackParams { get; set; }

        /// <summary>业务扩展参数
        /// </summary>
        [PayElement("extend_params", false)]
        public string ExtendParams { get; set; }

        /// <summary>可用渠道，用户只能在指定渠道范围内支付当有多个渠道时用“,”分隔
        /// </summary>
        [PayElement("enable_pay_channels", false)]
        public string EnablePayChannels { get; set; }

        /// <summary>禁用渠道，用户不可用指定渠道支付当有多个渠道时用“,”分隔
        /// </summary>
        [PayElement("disable_pay_channels", false)]
        public string DisablePayChannels { get; set; }

        /// <summary>商户门店编号
        /// </summary>
        [PayElement("store_id", false)]
        public string StoreId { get; set; }
        public PageTradeBizContentPayRequest()
        {

        }

        public PageTradeBizContentPayRequest(string subject, string body, string outTradeNo, string totalAmount)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
        }
    }
}
