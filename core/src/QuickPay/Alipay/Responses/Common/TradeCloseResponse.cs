using QuickPay.Infrastructure.RequestData;
using System;

namespace QuickPay.Alipay.Responses
{
    /// <summary>支付宝订单关闭
    /// </summary>
    public class TradeCloseResponse : AlipayCommonResponse
    {
        /// <summary>支付宝交易号
        /// </summary>
        [PayElement("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>商家订单号
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>是否需要重试
        /// </summary>
        [PayElement("retry_flag")]
        public string RetryFlag { get; set; }

        /// <summary>本次撤销触发的交易动作 close：关闭交易，无退款refund：产生了退款
        /// </summary>
        [PayElement("action")]
        public string Action { get; set; }

        /// <summary>当撤销产生了退款时,返回退款时间;默认不返回该信息,需与支付宝约定后配置返回;
        /// </summary>
        [PayElement("gmt_refund_pay",false)]
        public DateTime? RefundPay { get; set; }

        /// <summary>当撤销产生了退款时,返回的退款清算编号,用于清算对账使用;只在银行间联交易场景下返回该信息
        /// </summary>
        [PayElement("refund_settlement_id",false)]
        public string RefundSettlementId { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeCloseResponse()
        {

        }

    }
}
