using System;

namespace QuickPay.WechatPay.Url
{
    /// <summary>微信支付Url
    /// </summary>
    public interface IWechatPayUrl
    {
        /// <summary>H5下单地址
        /// </summary>
        string H5UnifiedOrderUrl { get; }

        /// <summary>JsApi下单地址
        /// </summary>
        string JsApiUnifiedOrderUrl { get; }

        /// <summary>App下单地址
        /// </summary>
        string AppUnifiedOrderUrl { get; }

        /// <summary>刷卡支付地址
        /// </summary>
        string MicropayUnifiedOrderUrl { get; }

        /// <summary>小程序支付地址
        /// </summary>
        string MiniProgramUnifiedOrderUrl { get; }

        /// <summary>扫码支付场景二地址
        /// </summary>
        string NativeMode2UnifiedOrderUrl { get; }

        /// <summary>退款地址
        /// </summary>
        string RefundQueryUrl { get; }

        /// <summary>下载订单地址
        /// </summary>
        string DownloadBillUrl { get; }

        /// <summary>订单关闭地址
        /// </summary>
        string OrderCloseUrl { get; }

        /// <summary>订单查询地址
        /// </summary>
        string OrderQueryUrl { get; }

        /// <summary>订单退款地址
        /// </summary>
        string OrderRefundUrl { get; }

        /// <summary>上报地址
        /// </summary>
        string ReportUrl { get; }

        /// <summary>获取请求类型的地址
        /// </summary>
        string GetRequestUrl(Type type);

    }
}
