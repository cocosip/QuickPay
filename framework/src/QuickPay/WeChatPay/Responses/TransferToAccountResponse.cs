using QuickPay.Infrastructure.RequestData;

namespace QuickPay.WeChatPay.Responses
{
    /// <summary>微信付款到帐号
    /// </summary>
    public class TransferToAccountResponse : WeChatPayCommonResponse
    {
        /// <summary>申请商户号的appid或商户号绑定的appid
        /// </summary>
        [PayElement("mch_appid")]
        public string AppId { get; set; }

        /// <summary>微信支付分配的商户号
        /// </summary>
        [PayElement("mchid")]
        public string MchId { get; set; }

        /// <summary>微信支付分配的终端设备号
        /// </summary>
        [PayElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>随机字符串
        /// </summary>
        [PayElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>商户订单号，需保持历史全局唯一性
        /// </summary>
        [PayElement("partner_trade_no")]
        public string TradeNo { get; set; }

        /// <summary>企业付款成功，返回的微信付款单号
        /// </summary>
        [PayElement("payment_no")]
        public string PaymentNo { get; set; }

        /// <summary>企业付款成功时间
        /// </summary>
        [PayElement("payment_time")]
        public string PaymentTime { get; set; }

    }
}
