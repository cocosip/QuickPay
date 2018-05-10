using QuickPay.Alipay.Responses;
using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Requests
{
    /// <summary>查询账单下载地址接口
    /// </summary>
    public class TradeBillDownloadUrlRequest : BaseAlipayRequest<TradeBillDownloadUrlResponse>
    {
        public override string Method => "alipay.data.dataservice.bill.downloadurl.query";

        [PayElement("app_auth_token", false)]
        public string AppAuthToken { get; set; }

        public TradeBillDownloadUrlRequest()
        {

        }


        public TradeBillDownloadUrlRequest(TradeBillDownloadUrlBizContentRequest bizContentRequest)
        {
            BizContentRequest = bizContentRequest;
        }


        public TradeBillDownloadUrlRequest(TradeBillDownloadUrlBizContentRequest bizContentRequest, string appAuthToken)
        {
            BizContentRequest = bizContentRequest;
            AppAuthToken = AppAuthToken;
        }

    }
}
