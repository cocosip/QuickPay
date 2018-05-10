using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Services.DTOs;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>交易订单查询
    /// </summary>
    [AutoMapTo(typeof(TradeQueryBizContentRequest))]
    public class TradeQueryInput: UniqueIdDto
    {
        /// <summary>	订单支付时传入的商户订单号,和支付宝交易号不能同时为空
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>支付宝交易号，和商户订单号不能同时为空(特殊可选)
        /// </summary>
        public string TradeNo { get; set; }

        public TradeQueryInput()
        {

        }
        public TradeQueryInput(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
