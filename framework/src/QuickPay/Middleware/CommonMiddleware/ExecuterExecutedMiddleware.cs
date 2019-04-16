using DotCommon.Http;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>执行器执行中间件
    /// </summary>
    public class ExecuterExecutedMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IHttpClient _httpClient;
        /// <summary>Ctor
        /// </summary>
        public ExecuterExecutedMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, IHttpClient httpClient)
        {
            _next = next;
            Logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            //执行器
            if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
            {
                try
                {
                    var response = await _httpClient.ExecuteAsync(context.HttpRequest);
                    context.HttpResponseString = response.Content;
                    Logger.LogInformation(context.Request.GetLogFormat($"执行Execute返回结果:[{response.Content}]"));
                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
                catch (Exception ex)
                {
                    SetPipelineError(context, new ExecuteError($"调用Execute出错,{ex.Message}"));
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
