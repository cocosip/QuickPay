using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>查询账单下载地址接口
    /// </summary>
    public class TradeBillDownloadUrlRequest : BaseAlipayRequest<TradeBillDownloadUrlResponse>
    {
        /// <summary>Method
        /// </summary>
        public override string Method => "alipay.data.dataservice.bill.downloadurl.query";

        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => AlipaySettings.ExtTradeType.TradeBillDownload;

        /// <summary>应用认证Token
        /// </summary>
        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        /// <summary>Ctor
        /// </summary>
        public TradeBillDownloadUrlRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public TradeBillDownloadUrlRequest(TradeBillDownloadUrlBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="bizContentRequest">bizContentRequest</param>
        /// <param name="appAuthToken">应用认证Token</param>
        public TradeBillDownloadUrlRequest(TradeBillDownloadUrlBizContentRequest bizContentRequest, string appAuthToken)
        {
            BizContentRequest = bizContentRequest;
            AppAuthToken = AppAuthToken;
        }

    }
}
