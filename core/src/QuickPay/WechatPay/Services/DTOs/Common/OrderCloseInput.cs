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
        /// <summary>>商户系统内部订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public OrderCloseInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">>商户系统内部订单号</param>
        public OrderCloseInput(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }
    }
}
