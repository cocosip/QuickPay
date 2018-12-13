using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WechatPay.Responses
{
    /// <summary>刷卡支付统一下单
    /// </summary>
    public class MicropayUnifiedOrderResponse : WechatPayCommonResponse
    {
        /********************以下字段在return_code为SUCCESS的时候有返回********************/

        /// <summary>微信分配的公众账号ID
        /// </summary>
        [PayElement("appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>随机字符串，不长于32位
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

        /*************************当return_code 和result_code都为SUCCESS的时*************************/

        /// <summary>用户OpenId
        /// </summary>
        [PayElement("openid")]
        public string OpenId { get; set; }

        /// <summary>用户是否关注公众账号
        /// </summary>
        [PayElement("is_subscribe")]
        public string IsSubscribe { get; set; }


        /// <summary>调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP
        /// </summary>
        [PayElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>银行类型
        /// </summary>
        [PayElement("bank_type")]
        public string BankType { get; set; }

        /// <summary>货币类型
        /// </summary>
        [PayElement("fee_type")]
        public string FeeType { get; set; }

        /// <summary>订单总金额
        /// </summary>
        [PayElement("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>当订单使用了免充值型优惠券后返回该参数
        /// </summary>
        [PayElement("settlement_total_fee", false)]
        public int SettlementTotalFee { get; set; }

        /// <summary>代金券金额
        /// </summary>
        [PayElement("coupon_fee", false)]
        public int CouponFee { get; set; }

        /// <summary>现金支付货币类型
        /// </summary>
        [PayElement("cash_fee_type", false)]
        public string CashFeeType { get; set; }

        /// <summary>订单现金支付金额
        /// </summary>
        [PayElement("cash_fee")]
        public int CashFee { get; set; }

        /// <summary>微信支付订单号
        /// </summary>
        [PayElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>商户系统内部订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>商家数据包，原样返回
        /// </summary>
        [PayElement("attach", false)]
        public string Attach { get; set; }

        /// <summary>支付完成时间	
        /// </summary>
        [PayElement("time_end")]
        public string TimeEnd { get; set; }

        /// <summary>营销详情
        /// </summary>
        [PayElement("promotion_detail", false)]
        public string PromotionDetail { get; set; }
    }
}
