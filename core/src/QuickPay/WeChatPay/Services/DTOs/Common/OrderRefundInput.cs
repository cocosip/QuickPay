using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WeChatPay.Requests;

namespace QuickPay.WeChatPay.Services.DTOs
{
    /// <summary>交易退款
    /// </summary>
    [AutoMapTo(typeof(OrderRefundRequest))]
    public class OrderRefundInput : UniqueIdDto
    {
        /// <summary>微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>货币类型，符合ISO 4217标准的三位字母代码
        /// </summary>
        public string RefundFeeType { get; set; }

        /// <summary>退款资金来源
        /// REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）
        /// REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款(限非当日交易订单的退款）
        /// </summary>
        public string RefundAccount { get; set; }


        /// <summary>商户中的订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>订单总金额,单位为分,只能为整数
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>退款总金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户中的订单号</param>
        /// <param name="outRefundNo">商户系统内部的退款单号</param>
        /// <param name="totalFee">订单总金额,单位为分,只能为整数</param>
        /// <param name="refundFee">退款总金额</param>
        public OrderRefundInput(string outTradeNo, string outRefundNo, int totalFee, int refundFee)
        {
            OutTradeNo = outTradeNo;
            OutRefundNo = outRefundNo;
            TotalFee = totalFee;
            RefundFee = refundFee;
        }
    }
}
