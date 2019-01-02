using DotCommon.AutoMapper;
using QuickPay.Infrastructure.Services.DTOs;
using QuickPay.WeChatPay.Requests;
using System;

namespace QuickPay.WeChatPay.Services.DTOs
{
    /// <summary>扫码支付模式1统一下单(返回预订单Id)
    /// </summary>
    [AutoMapTo(typeof(NativeMode1UnifiedOrderRequest))]
    public class NativeMode1UnifiedOrderInput : UniqueIdModel
    {
        /// <summary>商品简单描述，该字段请按照规范传递
        /// </summary>
        public string Body { get; set; }

        /// <summary>商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        public string SpbillCreateIp { get; set; }

        /// <summary>异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>异步通知类型
        /// </summary>
        public Type NotifyType { get; set; }

        /// <summary>Ctor
        /// </summary>
        public NativeMode1UnifiedOrderInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">订单总金额,单位为分</param>
        /// <param name="spbillCreateIp">APP和网页支付提交用户端ip</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public NativeMode1UnifiedOrderInput(string body, string outTradeNo, int totalFee, string spbillCreateIp, string notifyUrl)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIp = spbillCreateIp;
            NotifyUrl = notifyUrl;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="body">商品简单描述</param>
        /// <param name="outTradeNo">商户系统内部订单号</param>
        /// <param name="totalFee">订单总金额,单位为分</param>
        /// <param name="spbillCreateIp">APP和网页支付提交用户端ip</param>
        /// <param name="notifyType">通知类型</param>
        public NativeMode1UnifiedOrderInput(string body, string outTradeNo, int totalFee, string spbillCreateIp, Type notifyType)
        {
            Body = body;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIp = spbillCreateIp;
            NotifyType = notifyType;
        }
    }
}
