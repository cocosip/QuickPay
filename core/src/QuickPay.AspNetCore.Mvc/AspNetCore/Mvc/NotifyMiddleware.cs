using Microsoft.AspNetCore.Http;
using DotCommon.Logging;
using Microsoft.Extensions.Logging;

namespace QuickPay.AspNetCore.Mvc
{
    public class NotifyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<QuickPayLoggerName> _logger;

        public NotifyMiddleware(RequestDelegate next, ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            _logger = logger;
        }
        


    }
}
