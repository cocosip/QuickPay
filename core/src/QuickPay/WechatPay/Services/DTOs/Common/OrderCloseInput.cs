using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>关闭订单
    /// </summary>
    [AutoMapTo(typeof(OrderCloseRequest))]
    public class OrderCloseInput : UniqueIdDto
    {
        /// <summary>交易号
        /// </summary>
        public string OutTradeNo { get; set; }

        public OrderCloseInput()
        {

        }

        public OrderCloseInput(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
