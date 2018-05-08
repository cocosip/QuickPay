using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>退款查询
    /// </summary>
    public class RefundQueryRequest : BaseWechatPayRequest<RefundQueryResponse>
    {
        public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/refundquery";

        /// <summary>微信订单号
        /// </summary>
        [PayElement("transaction_id", false)]
        public string TransactionId { get; set; }

        /// <summary>商户系统内部订单号
        /// </summary>
        [PayElement("out_trade_no", false)]
        public string OutTradeNo { get; set; }

        /// <summary>商户退款单号
        /// </summary>
        [PayElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>微信退款单号
        /// </summary>
        [PayElement("refund_id", false)]
        public string RefundId { get; set; }

        public RefundQueryRequest()
        {

        }

        public RefundQueryRequest(string outRefundNo)
        {
            OutRefundNo = outRefundNo;
        }
    }
}
