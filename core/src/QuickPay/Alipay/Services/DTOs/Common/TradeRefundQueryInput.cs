using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>支付退款查询
    /// </summary>
    [AutoMapTo(typeof(TradeRefundQueryBizContentRequest))]
    public class TradeRefundQueryInput
    {
        /// <summary>支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>订单支付时传入的商户订单号,和支付宝交易号不能同时为空。
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>请求退款接口时,传入的退款请求号,如果在退款请求时未传入,则该值为创建交易时的外部交易号
        /// </summary>
        public string OutRequestNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeRefundQueryInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">订单支付时传入的商户订单号</param>
        /// <param name="outRequestNo">请求退款接口时,传入的退款请求号,如果在退款请求时未传入,则该值为创建交易时的外部交易号</param>
        public TradeRefundQueryInput(string outTradeNo, string outRequestNo)
        {
            OutTradeNo = outTradeNo;
            OutRequestNo = outRequestNo;
        }
    }
}
