using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.Requests;

namespace QuickPay.Alipay.Requests
{
    /// <summary>BizContent请求
    /// </summary>
    public class BaseBizContentRequest : BasePayRequest<DefaultBizContentResponse>
    {
        /// <summary>支付宝管道名
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.Alipay;

        /// <summary>签名字段名称
        /// </summary>
        public override string SignFieldName => AlipaySettings.DefaultSignFieldName;

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => "";

        /// <summary>签名类型名称
        /// </summary>
        public override string SignTypeName { get; set; } = "";
    }
}
