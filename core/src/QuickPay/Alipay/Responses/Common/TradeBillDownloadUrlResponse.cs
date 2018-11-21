using QuickPay.Infrastructure.RequestData;

namespace QuickPay.Alipay.Responses
{
    /// <summary>查询账单下载地址接口
    /// </summary>
    public class TradeBillDownloadUrlResponse : AlipayCommonResponse
    {
        /// <summary>账单下载地址链接，获取连接后30秒后未下载，链接地址失效
        /// </summary>
        [PayElement("bill_download_url")]
        public string BillDownloadUrl { get; set; }
        public TradeBillDownloadUrlResponse()
        {

        }
    }
}
