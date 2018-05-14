﻿using QuickPay.Infrastructure.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class ErrorHandlerMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public ErrorHandlerMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.IsError)
            {
                Logger.Error(context.Request.GetLogFormat(context.Errors.FirstOrDefault()?.Message));

                Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            await _next.Invoke(context);
        }

    }
}