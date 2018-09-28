using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class ErrorHandlerMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public ErrorHandlerMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            Logger = logger;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.IsError)
            {
                Logger.LogError(context.Request.GetLogFormat(context.Errors.FirstOrDefault()?.Message));

                Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            await _next.Invoke(context);
        }

    }
}
