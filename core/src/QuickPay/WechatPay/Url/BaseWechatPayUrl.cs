using QuickPay.WechatPay.Requests;
using System;
using System.Collections.Generic;

namespace QuickPay.WechatPay.Url
{
    public abstract class BaseWechatPayUrl : IWechatPayUrl
    {
        Dictionary<Type, string> RequestTypeUrlDict = new Dictionary<Type, string>();

        public BaseWechatPayUrl()
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

        public abstract string H5UnifiedOrderUrl { get; }

        public abstract string JsApiUnifiedOrderUrl { get; }

        public abstract string AppUnifiedOrderUrl { get; }

        public abstract string MicropayUnifiedOrderUrl { get; }

        public abstract string MiniProgramUnifiedOrderUrl { get; }

        public abstract string NativeMode2UnifiedOrderUrl { get; }

        public abstract string RefundQueryUrl { get; }

        public abstract string DownloadBillUrl { get; }

        public abstract string OrderCloseUrl { get; }

        public abstract string OrderQueryUrl { get; }

        public abstract string OrderRefundUrl { get; }
        public abstract string ReportUrl { get; }

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
