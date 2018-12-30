using QuickPay.WeChatPay.Requests;
using System;
using System.Collections.Generic;

namespace QuickPay.WeChatPay.Url
{
    /// <summary>微信支付请求基类地址
    /// </summary>
    public abstract class BaseWeChatPayUrl : IWeChatPayUrl
    {
        Dictionary<Type, string> RequestTypeUrlDict = new Dictionary<Type, string>();

        /// <summary>Ctor
        /// </summary>
        public BaseWeChatPayUrl()
        {
            RequestTypeUrlDict = new Dictionary<Type, string>();
            RequestTypeUrlDict.Add(typeof(AppUnifiedOrderRequest), AppUnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(H5UnifiedOrderRequest), H5UnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(JsApiUnifiedOrderRequest), JsApiUnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(MicropayUnifiedOrderRequest), MicropayUnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(MiniProgramUnifiedOrderRequest), MiniProgramUnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(NativeMode2UnifiedOrderRequest), NativeMode2UnifiedOrderUrl);
            RequestTypeUrlDict.Add(typeof(RefundQueryRequest), RefundQueryUrl);
            RequestTypeUrlDict.Add(typeof(DownloadBillRequest), DownloadBillUrl);
            RequestTypeUrlDict.Add(typeof(OrderCloseRequest), OrderCloseUrl);
            RequestTypeUrlDict.Add(typeof(OrderQueryRequest), OrderQueryUrl);
            RequestTypeUrlDict.Add(typeof(OrderRefundRequest), OrderRefundUrl);
            RequestTypeUrlDict.Add(typeof(ReportRequest), ReportUrl);
        }
        /// <summary>H5下单地址
        /// </summary>
        public abstract string H5UnifiedOrderUrl { get; }
        /// <summary>JsApi下单地址
        /// </summary>
        public abstract string JsApiUnifiedOrderUrl { get; }
        /// <summary>App下单地址
        /// </summary>
        public abstract string AppUnifiedOrderUrl { get; }
        /// <summary>刷卡支付地址
        /// </summary>
        public abstract string MicropayUnifiedOrderUrl { get; }

        /// <summary>小程序支付地址
        /// </summary>
        public abstract string MiniProgramUnifiedOrderUrl { get; }
        /// <summary>扫码支付场景二地址
        /// </summary>
        public abstract string NativeMode2UnifiedOrderUrl { get; }
        /// <summary>退款地址
        /// </summary>
        public abstract string RefundQueryUrl { get; }
        /// <summary>下载订单地址
        /// </summary>
        public abstract string DownloadBillUrl { get; }
        /// <summary>订单关闭地址
        /// </summary>
        public abstract string OrderCloseUrl { get; }
        /// <summary>订单查询地址
        /// </summary>
        public abstract string OrderQueryUrl { get; }
        /// <summary>订单退款地址
        /// </summary>
        public abstract string OrderRefundUrl { get; }

        /// <summary>上报地址
        /// </summary>
        public abstract string ReportUrl { get; }

        /// <summary>获取请求类型的地址
        /// </summary>
        public string GetRequestUrl(Type type)
        {
            if (RequestTypeUrlDict.ContainsKey(type))
            {
                return RequestTypeUrlDict[type];
            }
            return "";
        }
    }
}
