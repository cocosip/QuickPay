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
    public class ExecuteContext
    {
        public IPayRequest Request { get; set; }
        public PayResponse Response { get; set; }
        public QuickPayApp App { get; set; }
        public QuickPayConfig Config { get; set; }
        public PayData RequestPayData { get; set; }
        public PayData ResponsePayData { get; set; }

        public string RequestHandler { get; set; }
        public string SignType { get; set; }
        public string SignFieldName { get; set; }

        public IHttpRequest HttpRequest { get; set; }
        public string HttpResponseString { get; set; }
        public List<Error> Errors { get; } = new List<Error>();
        public bool IsError => Errors.Any();
    }
}
