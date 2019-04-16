using DotCommon.AutoMapper;
using QuickPay.Alipay.Requests;
using QuickPay.Infrastructure.Services.DTOs;
using System;

namespace QuickPay.Alipay.Services.DTOs
{
    /// <summary>扫码支付创建预订单
    /// </summary>
    [AutoMapTo(typeof(QrcodeTradeBizContentPayRequest))]
    public class QrcodePayPreCreateInput : UniqueIdModel
    {
        /// <summary>订单标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>对交易或商品的描述	
        /// </summary>
        public string Body { get; set; }

        /// <summary>商户网站唯一订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>通知类型
        /// </summary>
        public Type NotifyType { get; set; }

        /*******************************************/

        /// <summary>卖家支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>可打折金额. 参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】
        /// </summary>
        public decimal DiscountableAmount { get; set; }

        /// <summary>订单包含的商品列表信息.Json格式. 其它说明详见：“商品明细说明”
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

        /// <summary>该笔订单允许的最晚付款时间，逾期将关闭交易。
        /// </summary>
        public string TimeoutExpress { get; set; }

        /// <summary>Ctor
        /// </summary>
        public QrcodePayPreCreateInput()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="subject">商品的标题/交易标题/订单标题/订单关键字等</param>
        /// <param name="body">对一笔交易的具体描述信息</param>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位(如:1.00)</param>
        public QrcodePayPreCreateInput(string subject, string body, string outTradeNo, decimal totalAmount) : this(subject, body, outTradeNo, totalAmount, "")
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
        public QrcodePayPreCreateInput(string subject, string body, string outTradeNo, decimal totalAmount, string notifyUrl)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
            NotifyUrl = notifyUrl;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="subject">商品的标题/交易标题/订单标题/订单关键字等</param>
        /// <param name="body">对一笔交易的具体描述信息</param>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="totalAmount">订单总金额,单位为元,精确到小数点后两位(如:1.00)</param>
        /// <param name="notifyType">异步通知类型</param>
        public QrcodePayPreCreateInput(string subject, string body, string outTradeNo, decimal totalAmount, Type notifyType)
        {
            Subject = subject;
            Body = body;
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
            NotifyType = notifyType;
        }

    }
}
