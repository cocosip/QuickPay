namespace QuickPay.WeChatPay.Url
{
    /// <summary>微信沙盒环境地址
    /// </summary>
    public class SandboxWeChatPayUrl : BaseWeChatPayUrl
    {
        /// <summary>H5下单地址
        /// </summary>
        public override string H5UnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>JsApi下单地址
        /// </summary>
        public override string JsApiUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>App下单地址
        /// </summary>
        public override string AppUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>刷卡支付地址
        /// </summary>
        public override string MicropayUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>小程序支付地址
        /// </summary>
        public override string MiniProgramUnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>扫码支付场景二地址
        /// </summary>
        public override string NativeMode2UnifiedOrderUrl => "https://api.mch.weixin.qq.com/sendbox/pay/unifiedorder";

        /// <summary>退款地址
        /// </summary>
        public override string RefundQueryUrl => "https://api.mch.weixin.qq.com/sendbox/pay/refundquery";

        /// <summary>下载订单地址
        /// </summary>
        public override string DownloadBillUrl => "https://api.mch.weixin.qq.com/sendbox/pay/downloadbill";

        /// <summary>订单关闭地址
        /// </summary>
        public override string OrderCloseUrl => "https://api.mch.weixin.qq.com/sendbox/pay/closeorder";

        /// <summary>订单查询地址
        /// </summary>
        public override string OrderQueryUrl => "https://api.mch.weixin.qq.com/sendbox/pay/orderquery";

        /// <summary>订单退款地址
        /// </summary>
        public override string OrderRefundUrl => "https://api.mch.weixin.qq.com/sendbox/secapi/pay/refund";

        /// <summary>上报地址
        /// </summary>
        public override string ReportUrl => "https://api.mch.weixin.qq.com/sendbox/payitil/report";
    }
}
