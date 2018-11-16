namespace QuickPay.WechatPay.Url
{
    public class SandboxWechatPayUrl : BaseWechatPayUrl
    {
        public override string H5UnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string JsApiUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string AppUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string MicropayUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string MiniProgramUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string NativeMode2UnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        public override string RefundQueryUrl => "https://api.mch.weixin.qq.com/sendbox/pay/refundquery";

        public override string DownloadBillUrl => "https://api.mch.weixin.qq.com/sendbox/pay/downloadbill";

        public override string OrderCloseUrl => "https://api.mch.weixin.qq.com/sendbox/pay/closeorder";

        public override string OrderQueryUrl => "https://api.mch.weixin.qq.com/sendbox/pay/orderquery";

        public override string OrderRefundUrl => "https://api.mch.weixin.qq.com/sendbox/secapi/pay/refund";

        public override string ReportUrl => "https://api.mch.weixin.qq.com/sendbox/payitil/report";
    }
}
