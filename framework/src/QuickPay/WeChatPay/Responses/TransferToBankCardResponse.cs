using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WeChatPay.Responses
{
    /// <summary>转账到银行卡
    /// </summary>
    public class TransferToBankCardResponse : WeChatPayCommonResponse
    {
        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>商户订单号，需要保持唯一
        /// </summary>
        [PayElement("partner_trade_no")]
        public string PartnerTradeNo { get; set; }

        /// <summary>代付金额RMB:分
        /// </summary>
        [PayElement("amount")]
        public int Amount { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>返回包携带签名给商户
        /// </summary>
        [PayElement("sign")]
        public string Sign { get; set; }


    }
}
