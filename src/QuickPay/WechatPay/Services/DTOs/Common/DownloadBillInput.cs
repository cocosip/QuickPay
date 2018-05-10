using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WechatPay.Requests;

namespace QuickPay.WechatPay.Services.DTOs
{
    /// <summary>下载对账单
    /// </summary>
    [AutoMapTo(typeof(DownloadBillRequest))]
    public class DownloadBillInput : UniqueIdDto
    {
        /// <summary>对账日期
        /// </summary>
        public string BillDate { get; set; }

        /// <summary>账单类型
        /// </summary>
        public string BillType { get; set; }

        public DownloadBillInput()
        {

        }

        public DownloadBillInput(string billDate, string billType)
        {
            BillDate = billDate;
            BillType = billType;
        }

    }
}
