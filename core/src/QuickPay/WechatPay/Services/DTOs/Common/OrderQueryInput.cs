using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>支付查询订单输入
    /// </summary>
    [AutoMapTo(typeof(OrderQueryRequest))]
    public class OrderQueryInput : UniqueIdDto
    {
        /// <summary>微信的订单号，优先使用
        /// </summary>
        public string TransactionId { get; set; }


        /// <summary>商户系统内部的订单号,当没提供transaction_id时需要传这个
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public OrderQueryInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户系统内部的订单号,当没提供transaction_id时需要传这个</param>
        public OrderQueryInput(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
