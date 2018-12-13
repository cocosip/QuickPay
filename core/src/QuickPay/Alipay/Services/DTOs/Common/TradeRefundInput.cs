using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Services.DTOs;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>退款
    /// </summary>
    [AutoMapTo(typeof(TradeRefundBizContentRequest))]
    public class TradeRefundInput : UniqueIdDto
    {
        /// <summary>订单支付时传入的商户订单号,不能和 trade_no同时为空
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>需要退款的金额,该金额不能大于订单金额,单位为元,支持两位小数
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>退款原因
        /// </summary>
        public string RefundReason { get; set; }

        /// <summary>标识一次退款请求，同一笔交易多次退款需要保证唯一，如需部分退款，则此参数必传。
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>商户的操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>商户的门店编号
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>商户的终端编号
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeRefundInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">订单支付时传入的商户订单号</param>
        /// <param name="refundAmount">需要退款的金额,该金额不能大于订单金额,单位为元,支持两位小数</param>
        public TradeRefundInput(string outTradeNo, decimal refundAmount)
        {
            OutTradeNo = outTradeNo;
            RefundAmount = refundAmount;
        }
    }
}
