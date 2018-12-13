using DotCommon.Http;
using Microsoft.Extensions.Logging;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WechatPay.Url;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    /// <summary>微信支付请求创建中间件
    /// </summary>
    public class WechatPayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly QuickPayConfigurationOption _option;
        private readonly IWechatPayUrl _wechatPayUrl;
        /// <summary>Ctor
        /// </summary>
        public WechatPayRequestBuilderMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, QuickPayConfigurationOption option, IWechatPayUrl wechatPayUrl)
        {
            _next = next;
            Logger = logger;
            _option = option;
            _wechatPayUrl = wechatPayUrl;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
                {
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var requestXml = context.RequestPayData.ToXml();
                        var requestUrl = _wechatPayUrl.GetRequestUrl(context.Request.GetType());
                        if (requestUrl == null)
                        {
                            SetPipelineError(context, new ExecuteError($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url"));
                            return;
                        }
                        //var urlProperty = context.Request.GetType().GetProperties().FirstOrDefault(x => x.Name == "RequestUrl");
                        //if (urlProperty == null)
                        //{
                        //    SetPipelineError(context, new ExecuteError($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url"));
                        //    return;
                        //}
                        //准备Http请求
                        //IHttpRequest httpRequest = new HttpRequest(urlProperty.GetValue(context.Request).ToString(), Method.POST);
                        IHttpRequest httpRequest = new HttpRequest(requestUrl, Method.POST);
                        httpRequest.AddXmlBody(requestXml);
                        context.HttpRequest = httpRequest;
                    }

                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
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
