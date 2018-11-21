namespace QuickPay.WechatPay.Url
{
    /// <summary>真实环境中,各个请求Url
    /// </summary>
    public class RealWechatPayUrl : BaseWechatPayUrl
    {
        public override string H5UnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string JsApiUnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string AppUnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string MicropayUnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string MiniProgramUnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string NativeMode2UnifiedOrderUrl => "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public override string RefundQueryUrl => "https://api.mch.weixin.qq.com/pay/refundquery";

        public override string DownloadBillUrl => "https://api.mch.weixin.qq.com/pay/downloadbill";

        public override string OrderCloseUrl => "https://api.mch.weixin.qq.com/pay/closeorder";

        public override string OrderQueryUrl => "https://api.mch.weixin.qq.com/pay/orderquery";

        public override string OrderRefundUrl => "https://api.mch.weixin.qq.com/secapi/pay/refund";

        public override string ReportUrl => "https://api.mch.weixin.qq.com/payitil/report";
    }
}
