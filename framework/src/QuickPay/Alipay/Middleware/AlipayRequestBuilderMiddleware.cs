using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    /// <summary>支付宝请求构建中间件
    /// </summary>
    public class AlipayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        /// <summary>Ctor
        /// </summary>
        public AlipayRequestBuilderMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next) : base(provider)
        {
            _next = next;
        }
        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        HttpBuilder builder = new HttpBuilder(Method.POST);
                        //构建Http
                        foreach (var pValue in context.RequestPayData.GetValues())
                        {
                            builder.AddParameter(pValue.Key, pValue.Value);
                        }
                        context.HttpBuilder = builder;

                        Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                    }
                }

            }
            catch (Exception ex)
            {
                SetPipelineError(context, new ExecuteError($"构建RequestBuilder错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }


    }
}
