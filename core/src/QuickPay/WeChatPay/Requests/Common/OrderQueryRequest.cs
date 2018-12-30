using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WeChatPay.Responses;

namespace QuickPay.WeChatPay.Requests
{
    /// <summary>查询订单
    /// </summary>
    public class OrderQueryRequest : BaseWeChatPayRequest<OrderQueryResponse>
    {
        //public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WeChatPaySettings.ExtTradeType.OrderQuery;


        /// <summary>微信的订单号，优先使用
        /// </summary>
        [PayElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>商户系统内部的订单号,当没提供transaction_id时需要传这个
        /// </summary>
        [PayElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>Ctor
        /// </summary>
        public OrderQueryRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户系统内部的订单号,当没提供transaction_id时需要传这个</param>
        public OrderQueryRequest(string outTradeNo)
        {
            OutTradeNo = outTradeNo;
        }

    }
}
