using DotCommon.Http;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class ExecuterExecutedMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IHttpClient _httpClient;
        public ExecuterExecutedMiddleware(QuickPayExecuteDelegate next, IHttpClient httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task Invoke(ExecuteContext context)
        {
            //执行器
            if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
            {
                try
                {
                    var response = await _httpClient.ExecuteAsync(context.RequestBuilder);
                    context.HttpResponseString = response.GetResponseString();
                    Logger.Info(context.Request.GetLogFormat($"执行Execute返回结果:[{response.GetResponseString()}]"));
                }
                catch (Exception ex)
                {
                    Logger.Error(context.Request.GetLogFormat($"调用Execute出错,{ex.Message}"), ex);
                    SetPipelineError(context, new ExecuteError("调用远程服务出错"));
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
