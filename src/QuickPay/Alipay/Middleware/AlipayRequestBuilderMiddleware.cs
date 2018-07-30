using DotCommon.Http;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
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
        public AlipayRequestBuilderMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
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
                        //发送http请求
                        IHttpRequest httpRequest = new HttpRequest(config.Gateway, Method.POST);
                        foreach (var pValue in context.RequestPayData.GetValues())
                        {
                            httpRequest.AddParameter(pValue.Key, pValue.Value);
                        }
                        context.HttpRequest = httpRequest;

                        Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"构建RequestBuilder错误,{ex.Message}"));
                SetPipelineError(context, new ExecuteError("支付宝构建RequestBuilder错误"));
                return;
            }
            await _next.Invoke(context);
        }


    }
}
