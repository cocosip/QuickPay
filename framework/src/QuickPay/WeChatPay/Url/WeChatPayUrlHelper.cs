using QuickPay.WeChatPay.Requests;
using System;
using System.Collections.Generic;

namespace QuickPay.WeChatPay.Url
{
    /// <summary>微信支付Url相关操作
    /// </summary>
    public static class WeChatPayUrlHelper
    {
        static readonly Dictionary<Type, string> RequestTypeUrlDict = new Dictionary<Type, string>()
        {
            { typeof(AppUnifiedOrderRequest), WeChatPaySettings.Resources.AppUnifiedOrder },
            { typeof(H5UnifiedOrderRequest), WeChatPaySettings.Resources.H5UnifiedOrder },
            { typeof(JsApiUnifiedOrderRequest), WeChatPaySettings.Resources.JsApiUnifiedOrder },
            { typeof(MicropayUnifiedOrderRequest), WeChatPaySettings.Resources.MicropayUnifiedOrder },
            { typeof(MiniProgramUnifiedOrderRequest), WeChatPaySettings.Resources.MiniProgramUnifiedOrder },
            { typeof(NativeMode2UnifiedOrderRequest), WeChatPaySettings.Resources.NativeMode2UnifiedOrder },
            { typeof(RefundQueryRequest), WeChatPaySettings.Resources.RefundQuery },
            { typeof(DownloadBillRequest), WeChatPaySettings.Resources.DownloadBill },
            { typeof(OrderCloseRequest), WeChatPaySettings.Resources.OrderClose },
            { typeof(OrderQueryRequest), WeChatPaySettings.Resources.OrderQuery },
            { typeof(OrderRefundRequest), WeChatPaySettings.Resources.OrderRefund },
            { typeof(ReportRequest), WeChatPaySettings.Resources.Report },
            { typeof(TransferToAccountRequest), WeChatPaySettings.Resources.TransferToAccount },
            { typeof(TransferToBankCardRequest), WeChatPaySettings.Resources.TransferToBank }
        };

        /// <summary>获取请求类型的Resource
        /// </summary>
        public static string GetRequestResource(Type type)
        {
            if (RequestTypeUrlDict.ContainsKey(type))
            {
                return RequestTypeUrlDict[type];
            }
            return "";
        }
    }
}
