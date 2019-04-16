using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Services.DTOs;
using System;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>PC网站支付
    /// </summary>

    [AutoMapTo(typeof(PageTradeBizContentPayRequest))]
    public class PageTradePayInput : UniqueIdModel
    {
        /// <summary>对一笔交易的具体描述信息
        /// </summary>
        public string Body { get; set; }

        /// <summary>商品的标题/交易标题/订单标题/订单关键字等
        /// </summary>
        public string Subject { get; set; }

        /// <summary>商户网站唯一订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额，单位为元，精确到小数点后两位
        /// </summary>
        public string TotalAmount { get; set; }


        //////////////////////////////////////////////////////////////////////////


        /// <summary>设置未付款支付宝交易的超时时间，一旦超时，该笔交易就会自动被关闭
        /// 该参数数值不接受小数点， 如 1.5h，可转换为 90m
        /// </summary>
        public string TimeoutExpress { get; set; }

        /// <summary>收款支付宝用户ID。
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>商品类型
        /// </summary>
        public string GoodType { get; set; }

        /// <summary>公用回传参数，如果请求时传递了该参数，则返回给商户时会回传该参数
        /// </summary>
        public string PassbackParams { get; set; }

        /// <summary>业务扩展参数
        /// </summary>
        public string ExtendParams { get; set; }

        /// <summary>可用渠道，用户只能在指定渠道范围内支付当有多个渠道时用“,”分隔
        /// </summary>
        public string EnablePayChannels { get; set; }

        /// <summary>禁用渠道，用户不可用指定渠道支付当有多个渠道时用“,”分隔
        /// </summary>
        public string DisablePayChannels { get; set; }

        /// <summary>商户门店编号
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>同步跳转通知页面
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>通知地址
        /// </summary>
        public Type NotifyType { get; set; }

        /// <summary>Ctor
        /// </summary>
        public PageTradePayInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="subject">商品的标题/交易标题/订单标题/订单关键字等</param>
        /// <param name="body">对一笔交易的具体描述信息</param>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位(如:1.00)</param>
        public PageTradePayInput(string subject, string body, string outTradeNo, string totalAmount)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
        }


        /// <summary>Ctor
        /// </summary>
        /// <param name="subject">商品的标题/交易标题/订单标题/订单关键字等</param>
        /// <param name="body">对一笔交易的具体描述信息</param>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位(如:1.00)</param>
        /// <param name="notifyUrl">异步通知地址</param>
        /// <param name="returnUrl">同步返回地址</param>  
        public PageTradePayInput(string subject, string body, string outTradeNo, string totalAmount, string notifyUrl, string returnUrl)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
            NotifyUrl = notifyUrl;
            ReturnUrl = returnUrl;
        }
        /// <summary>Ctor
        /// </summary>
        /// <param name="subject">商品的标题/交易标题/订单标题/订单关键字等</param>
        /// <param name="body">对一笔交易的具体描述信息</param>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位(如:1.00)</param>
        /// <param name="notifyType">异步通知类型</param>
        /// <param name="returnUrl">同步返回地址</param>  
        public PageTradePayInput(string subject, string body, string outTradeNo, string totalAmount, Type notifyType, string returnUrl)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
            NotifyType = notifyType;
            ReturnUrl = returnUrl;
        }



    }
}
