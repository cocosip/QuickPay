using QuickPay.Alipay.Responses;

namespace QuickPay.Alipay.Requests
{
    /// <summary>订单关闭
    /// </summary>
    public class TradeCloseRequest : BaseAlipayRequest<TradeCloseResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.trade.close";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeClose;

        /// <summary>Ctor
        /// </summary>
        public TradeCloseRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        public TradeCloseRequest(TradeCloseBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
