using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>关闭订单
    /// </summary>
    public class OrderCloseRequest : BaseWechatPayRequest<OrderCloseResponse>
    {
        //public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/closeorder";
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.ExtTradeType.OrderClose;

        /// <summary>商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public OrderCloseRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        public OrderCloseRequest(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }


    }
}
