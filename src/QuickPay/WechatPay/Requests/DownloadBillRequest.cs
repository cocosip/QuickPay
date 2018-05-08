using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Responses;

namespace QuickPay.WechatPay.Requests
{
    /// <summary>下载对账单
    /// </summary>
    public class DownloadBillRequest : BaseWechatPayRequest<DownloadBillResponse>
    {
        public override string RequestUrl => "https://api.mch.weixin.qq.com/pay/downloadbill";

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

        public override void SetNecessary(WechatPayConfig config, WechatPayApp app)
        {
            base.SetNecessary(config, app);
            SignType = config.SignType;
        }

        public DownloadBillRequest()
        {

        }

        public DownloadBillRequest(string tarType, string billDate)
        {
            TarType = tarType;
            BillDate = billDate;
        }
    }
}
