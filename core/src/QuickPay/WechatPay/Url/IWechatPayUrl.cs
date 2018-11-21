using System;

namespace QuickPay.WechatPay.Url
{
    public interface IWechatPayUrl
    {
        string H5UnifiedOrderUrl { get; }
        string JsApiUnifiedOrderUrl { get; }
        string AppUnifiedOrderUrl { get; }
        string MicropayUnifiedOrderUrl { get; }
        string MiniProgramUnifiedOrderUrl { get; }
        string NativeMode2UnifiedOrderUrl { get; }
        string RefundQueryUrl { get; }
        string DownloadBillUrl { get; }
        string OrderCloseUrl { get; }
        string OrderQueryUrl { get; }
        string OrderRefundUrl { get; }
        string ReportUrl { get; }
        string GetRequestUrl(Type type);

    }
}
