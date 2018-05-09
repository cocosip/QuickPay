using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>退款
    /// </summary>
    public class OrderRefundResponse : WechatPayCommonResponse
    {
        /// <summary>公众号Id
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>签名字段
        /// </summary>
        [PayElement("sign")]
        public string Sign { get; set; }

        /// <summary>微信订单号
        /// </summary>
        [PayElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>商户系统内部订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>商户退款单号
        /// </summary>
        [PayElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>微信退款单号	
        /// </summary>
        [PayElement("refund_id")]
        public string RefundId { get; set; }

        /// <summary>退款金额	
        /// </summary>
        [PayElement("refund_fee")]
        public int RefundFee { get; set; }

        /// <summary>应结退款金额
        /// 去掉非充值代金券退款金额后的退款金额，退款金额=申请退款金额-非充值代金券退款金额，退款金额<= 申请退款金额
        /// </summary>
        [PayElement("settlement_refund_fee", false)]
        public int SettlementRefundFee { get; set; }

        /// <summary>订单总金额
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>应结订单金额
        /// </summary>
        [PayElement("settlement_total_fee", false)]
        public int SettlementTotalFee { get; set; }

        /// <summary>标价币种
        /// </summary>
        [PayElement("fee_type", false)]
        public string FeeType { get; set; }

        /// <summary>现金支付金额
        /// </summary>
        [PayElement("cash_fee")]
        public int CashFee { get; set; }

        /// <summary>现金支付币种
        /// </summary>
        [PayElement("cash_fee_type", false)]
        public string CashFeeType { get; set; }

        /// <summary>现金退款金额
        /// </summary>
        [PayElement("cash_refund_fee", false)]
        public string CashRefundFee { get; set; }

        /// <summary>代金券退款总金额
        /// </summary>
        [PayElement("coupon_refund_fee", false)]
        public int CouponRefundFee { get; set; }

        /// <summary>退款代金券使用数量
        /// </summary>
        [PayElement("coupon_refund_count", false)]
        public int CouponRefundCount { get; set; }

        public OrderRefundResponse()
        {

        }

    }
}
