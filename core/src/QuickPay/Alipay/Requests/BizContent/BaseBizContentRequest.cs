using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.Requests;

namespace QuickPay.Alipay.Requests
{
    /// <summary>BizContent请求
    /// </summary>
    public class BaseBizContentRequest : BasePayRequest<DefaultBizContentResponse>
    {
        public override string Provider => QuickPaySettings.Provider.Alipay;

        public override string SignFieldName => AlipaySettings.DefaultSignFieldName;
        public override string TradeTypeName => "";

        public override string SignTypeName => "";
    }
}
