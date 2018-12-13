using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>下载对账单
    /// </summary>
    public class DownloadBillRequest : BaseWechatPayRequest<DownloadBillResponse>
    {
        /// <summary>交易类型名称
        /// </summary>
        public override string TradeTypeName => WechatPaySettings.ExtTradeType.DownloadBill;

        /// <summary>设备号
        /// </summary>
        [PayElement("device_info", false)]
        public string DeviceInfo { get; set; }

        /// <summary>签名类型
        /// </summary>
        [PayElement("sign_type", false)]
        public string SignType { get; set; }

        /// <summary>压缩账单,固定为GZIP
        /// </summary>

        [PayElement("tar_type", false)]
        public string TarType { get; set; } = WechatPaySettings.TarType.Gzip;

        /// <summary>对账日期
        /// </summary>
        [PayElement("bill_date")]
        public string BillDate { get; set; }

        /// <summary>账单类型
        /// </summary>
        [PayElement("bill_type")]
        public string BillType { get; set; }

        /// <summary>设置必要参数
        /// </summary>
        public override void SetNecessary(QuickPayConfig config, QuickPayApp app)
        {
            base.SetNecessary(config, app);
            SignType = ((WechatPayConfig)config).SignType;
        }

        /// <summary>Ctor
        /// </summary>
        public DownloadBillRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="tarType">压缩账单,固定为GZIP</param>
        /// <param name="billDate">账单日期</param>
        public DownloadBillRequest(string tarType, string billDate)
        {
            TarType = tarType;
            BillDate = billDate;
        }
    }
}
