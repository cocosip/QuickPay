using QuickPay.Alipay.Responses;

namespace QuickPay.Alipay.Requests
{
    /// <summary>订单关闭
    /// </summary>
    public class TradeCloseRequest:BaseAlipayRequest<TradeCloseResponse>
    {
        public override string Method => "alipay.trade.close";

        public TradeCloseRequest()
        {

        }

        public TradeCloseRequest(TradeCloseBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }
    }
}
