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
            RequestTypeUrlDict = new Dictionary<Type, string>
            {
                { typeof(AppUnifiedOrderRequest), AppUnifiedOrderUrl },
                { typeof(H5UnifiedOrderRequest), H5UnifiedOrderUrl },
                { typeof(JsApiUnifiedOrderRequest), JsApiUnifiedOrderUrl },
                { typeof(MicropayUnifiedOrderRequest), MicropayUnifiedOrderUrl },
                { typeof(MiniProgramUnifiedOrderRequest), MiniProgramUnifiedOrderUrl },
                { typeof(NativeMode2UnifiedOrderRequest), NativeMode2UnifiedOrderUrl },
                { typeof(RefundQueryRequest), RefundQueryUrl },
                { typeof(DownloadBillRequest), DownloadBillUrl },
                { typeof(OrderCloseRequest), OrderCloseUrl },
                { typeof(OrderQueryRequest), OrderQueryUrl },
                { typeof(OrderRefundRequest), OrderRefundUrl },
                { typeof(ReportRequest), ReportUrl },
                { typeof(TransferToAccountRequest), TransferToAccountUrl }
            };

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

        /// <summary>企业付款到帐号地址
        /// </summary>
        public abstract string TransferToAccountUrl { get; }

        /// <summary>企业付款到银行卡地址
        /// </summary>
        public abstract string TransferToBankUrl { get; }

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
