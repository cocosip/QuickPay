using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>交易查询接口
    /// </summary>
    public class TradeQueryResponse : AlipayCommonResponse
    {
        // <summary>支付宝交易号
        /// </summary>
        [PayElement("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>商家订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>买家支付宝账号
        /// </summary>
        [PayElement("buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        /// <summary>交易状态
        /// </summary>
        [PayElement("trade_status")]
        public string TradeStatus { get; set; }

        /// <summary>交易的订单金额，单位为元，两位小数。该参数的值为支付时传入的total_amount
        /// </summary>
        [PayElement("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>实收金额，单位为元，两位小数。该金额为本笔交易，商户账户能够实际收到的金额
        /// </summary>
        [PayElement("receipt_amount")]
        public string ReceiptAmount { get; set; }

        /// <summary>买家实付金额，单位为元，两位小数。该金额代表该笔交易买家实际支付的金额，不包含商户折扣等金额
        /// </summary>
        [PayElement("buyer_pay_amount", false)]
        public decimal BuyerPayAmount { get; set; }

        /// <summary>积分支付的金额，单位为元，两位小数。该金额代表该笔交易中用户使用积分支付的金额，比如集分宝或者支付宝实时优惠等
        /// </summary>
        [PayElement("point_amount", false)]
        public decimal PointAmount { get; set; }

        /// <summary>交易中用户支付的可开具发票的金额，单位为元，两位小数。该金额代表该笔交易中可以给用户开具发票的金额
        /// </summary>
        [PayElement("invoice_amount", false)]
        public decimal InvoiceAmount { get; set; }

        /// <summary>本次交易打款给卖家的时间
        /// </summary>
        [PayElement("send_pay_date")]
        public string SendPayDate { get; set; }

        /// <summary>支付宝店铺编号
        /// </summary>
        [PayElement("alipay_store_id", false)]
        public string AlipayStoreId { get; set; }

        /// <summary>商户门店编号
        /// </summary>
        [PayElement("store_id", false)]
        public string StoreId { get; set; }

        /// <summary>商户机具终端编号
        /// </summary>
        [PayElement("terminal_id", false)]
        public string TerminalId { get; set; }

        /// <summary>交易支付使用的资金渠道
        /// </summary>
        [PayElement("fund_bill_list")]
        public string FundBillList { get; set; }

        /// <summary>请求交易支付中的商户店铺的名称
        /// </summary>
        [PayElement("store_name", false)]
        public string SotreName { get; set; }

        /// <summary>买家在支付宝的用户id
        /// </summary>
        [PayElement("buyer_user_id")]
        public string BuyerUserId { get; set; }

        /// <summary>本次交易支付所使用的单品券优惠的商品优惠信息
        /// </summary>
        [PayElement("discount_goods_detail")]
        public string DiscountGoodsDetail { get; set; }

        /// <summary>行业特殊信息（例如在医保卡支付业务中，向用户返回医疗信息）。
        /// </summary>
        [PayElement("industry_sepc_detail", false)]
        public string IndustrySepcDetail { get; set; }

        /// <summary>本交易支付时使用的所有优惠券信息
        /// </summary>
        [PayElement("voucher_detail_list", false)]
        public string VoucherDetailList { get; set; }

        /// <summary>买家支付宝用户号，该字段将废弃，不要使用
        /// </summary>
        [PayElement("open_id", false)]
        public string OpenId { get; set; }

        public TradeQueryResponse()
        {

        }
    }
}
