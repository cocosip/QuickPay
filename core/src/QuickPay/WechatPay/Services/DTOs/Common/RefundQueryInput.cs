using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>App查询退款订单
    /// </summary>
    [AutoMapTo(typeof(RefundQueryRequest))]
    public class RefundQueryInput : UniqueIdDto
    {
        /// <summary>微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>商户系统内部订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>微信退款单号
        /// </summary>
        public string RefundId { get; set; }

        public RefundQueryInput()
        {

        }

        public RefundQueryInput(string outRefundNo)
        {
            OutRefundNo = outRefundNo;
        }
    }
}
