using DotCommon.Http;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    public class AlipayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly QuickPayConfigurationOption _option;
        public AlipayRequestBuilderMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, QuickPayConfigurationOption option)
        {
            _next = next;
            Logger = logger;
            _option = option;
        }

        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var app = (AlipayApp)context.App;
                        var config = (AlipayConfig)context.Config;
                        var gateway = _option.EnabledAlipaySandbox ? config.SandboxGateway : config.Gateway;

                        //构建Http
                        IHttpRequest httpRequest = new HttpRequest(gateway, Method.POST);
                        foreach (var pValue in context.RequestPayData.GetValues())
                        {
                            httpRequest.AddParameter(pValue.Key, pValue.Value);
                        }
                        context.HttpRequest = httpRequest;

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
