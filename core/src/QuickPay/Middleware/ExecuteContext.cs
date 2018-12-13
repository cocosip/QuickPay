using DotCommon.Http;
using QuickPay.Errors;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Middleware
{
    /// <summary>执行上下文
    /// </summary>
    public class ExecuteContext
    {
        /// <summary>请求
        /// </summary>
        public IPayRequest Request { get; set; }

        /// <summary>响应
        /// </summary>
        public PayResponse Response { get; set; }

        /// <summary>应用App
        /// </summary>
        public QuickPayApp App { get; set; }

        /// <summary>当前支付管道配置信息
        /// </summary>
        public QuickPayConfig Config { get; set; }

        /// <summary>请求PayData
        /// </summary>
        public PayData RequestPayData { get; set; }

        /// <summary>响应PayData
        /// </summary>
        public PayData ResponsePayData { get; set; }

        /// <summary>请求处理方式
        /// </summary>
        public string RequestHandler { get; set; }

        /// <summary>签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>签名字段名
        /// </summary>
        public string SignFieldName { get; set; }

        /// <summary>Http请求所需数据
        /// </summary>
        public IHttpRequest HttpRequest { get; set; }

        /// <summary>Http响应字符串
        /// </summary>
        public string HttpResponseString { get; set; }

        /// <summary>错误信息
        /// </summary>
        public List<Error> Errors { get; } = new List<Error>();

        /// <summary>是否有错误
        /// </summary>
        public bool IsError => Errors.Any();
    }
}
