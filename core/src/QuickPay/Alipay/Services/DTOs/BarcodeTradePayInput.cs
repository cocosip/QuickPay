using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Services.DTOs;
using System;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>条码支付
    /// </summary>
    [AutoMapTo(typeof(BarcodeTradeBizContentPayRequest))]
    public class BarcodeTradePayInput : UniqueIdDto
    {

        /// <summary>商户网站唯一订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>支付场景
        /// </summary>
        public string Scene { get; set; }

        /// <summary>支付授权码，25~30开头的长度为16~24位的数字，实际字符串长度以开发者获取的付款码长度为准
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>订单标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>订单描述
        /// </summary>
        public string Body { get; set; }

        /// <summary>订单总金额，单位为元，精确到小数点后两位
        /// 如果同时传入【可打折金额】和【不可打折金额】，该参数可以不用传入； 
        /// 如果同时传入了【可打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【可打折金额】+【不可打折金额】
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>通知类型
        /// </summary>
        public Type NotifyType { get; set; }



        /*****************************************/
        /// <summary>销售产品码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>买家的支付宝用户id，如果为空，会从传入了码值信息中获取买家ID
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>参与优惠计算的金额，单位为元，精确到小数点后两位
        /// </summary>
        public decimal DiscountableAmount { get; set; }

        /// <summary>订单包含的商品列表信息，Json格式，其它说明详见商品明细说明
        /// </summary>
        public string GoodsDetail { get; set; }

        /// <summary>商户操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>商户门店编号
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>商户机具终端编号
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>业务扩展参数
        /// </summary>
        public string ExtendParams { get; set; }

        /// <summary>该笔订单允许的最晚付款时间，逾期将关闭交易
        /// </summary>
        public string TimeoutExpress { get; set; }

        /// <summary>Ctor
        /// </summary>
        public BarcodeTradePayInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="scene">支付场景</param>
        /// <param name="authCode">支付授权码</param>
        /// <param name="subject">订单标题</param>
        /// <param name="body">订单描述</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位</param>
        public BarcodeTradePayInput(string outTradeNo, string scene, string authCode, string subject, string body, decimal totalAmount)
        {
            OutTradeNo = outTradeNo;
            Scene = scene;
            AuthCode = authCode;
            Subject = subject;
            Body = body;
            TotalAmount = totalAmount;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="scene">支付场景</param>
        /// <param name="authCode">支付授权码</param>
        /// <param name="subject">订单标题</param>
        /// <param name="body">订单描述</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位</param>
        /// <param name="notifyUrl">异步通知地址</param>
        public BarcodeTradePayInput(string outTradeNo, string scene, string authCode, string subject, string body, decimal totalAmount, string notifyUrl)
        {
            OutTradeNo = outTradeNo;
            Scene = scene;
            AuthCode = authCode;
            Subject = subject;
            Body = body;
            TotalAmount = totalAmount;
            NotifyUrl = notifyUrl;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="scene">支付场景</param>
        /// <param name="authCode">支付授权码</param>
        /// <param name="subject">订单标题</param>
        /// <param name="body">订单描述</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位</param>
        /// <param name="notifyType">异步通知类型</param>
        public BarcodeTradePayInput(string outTradeNo, string scene, string authCode, string subject, string body, decimal totalAmount, Type notifyType)
        {
            OutTradeNo = outTradeNo;
            Scene = scene;
            AuthCode = authCode;
            Subject = subject;
            Body = body;
            TotalAmount = totalAmount;
            NotifyType = notifyType;
        }
    }
}
