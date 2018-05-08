using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>订单查询
    /// </summary>
    public class OrderQueryResponse : BaseWechatPayResponse
    {
        /// <summary>应用Id
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>签名
        /// </summary>
        [PayElement("sign")]
        public string Sign { get; set; }


        /// <summary>设备号
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>用户在商户appid下的唯一标识
        /// </summary>
        [PayElement("openid")]
        public string OpenId { get; set; }

        /// <summary>用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        [PayElement("is_subscribe", false)]
        public string IsSubscribe { get; set; }

        /// <summary>交易类型
        /// </summary>
        [PayElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// 交易状态
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（刷卡支付）
        /// USERPAYING--用户支付中
        /// PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        [PayElement("trade_state")]
        public string TradeState { get; set; }

        /// <summary>银行类型，采用字符串类型的银行标识
        /// </summary>
        [PayElement("bank_type")]
        public string BankType { get; set; }

        /// <summary>订单总金额，单位为分
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>货币类型，符合ISO 4217标准的三位字母代码
        /// </summary>
        [PayElement("fee_type", false)]
        public string FeeType { get; set; }

        /// <summary>现金支付金额订单现金支付金额
        /// </summary>
        [PayElement("cash_fee")]
        public int CashFee { get; set; }

        /// <summary>货币类型，符合ISO 4217标准的三位字母代码
        /// </summary>
        [PayElement("cash_fee_type", false)]
        public string CashFeeType { get; set; }

        /// <summary>应结订单金额,当订单使用了免充值型优惠券后返回该参数
        /// </summary>
        [PayElement("settlement_total_fee", false)]
        public string SettlementTotalFee { get; set; }

        /// <summary>代金券金额
        /// </summary>
        [PayElement("coupon_fee", false)]
        public int CouponFee { get; set; }

        /// <summary>代金券使用数量
        /// </summary>
        [PayElement("coupon_count", false)]
        public int CouponCount { get; set; }

        /// <summary>微信支付订单号
        /// </summary>
        [PayElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>商户订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>附加数据，原样返回
        /// </summary>
        [PayElement("attach", false)]
        public string Attach { get; set; }

        /// <summary>支付完成时间
        /// </summary>
        [PayElement("time_end")]
        public string TimeEnd { get; set; }

        /// <summary>交易状态描述
        /// </summary>
        [PayElement("trade_state_desc")]
        public string TradeStateDesc { get; set; }


        public OrderQueryResponse()
        {

        }
    }
}
