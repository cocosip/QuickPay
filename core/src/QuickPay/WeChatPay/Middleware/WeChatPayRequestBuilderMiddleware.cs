using DotCommon.Http;
using Microsoft.Extensions.Logging;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Url;
using QuickPay.WeChatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Middleware
{
    /// <summary>微信支付请求创建中间件
    /// </summary>
    public class WeChatPayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly QuickPayConfigurationOption _option;
        private readonly IWeChatPayUrl _wechatPayUrl;
        private readonly WeChatPayDataHelper _wechatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WeChatPayRequestBuilderMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, QuickPayConfigurationOption option, IWeChatPayUrl wechatPayUrl, WeChatPayDataHelper wechatPayDataHelper)
        {
            _next = next;
            Logger = logger;
            _option = option;
            _wechatPayUrl = wechatPayUrl;
            _wechatPayDataHelper = wechatPayDataHelper;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.WeChatPay)
                {
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var requestXml = _wechatPayDataHelper.ToXml(context.RequestPayData);
                        var requestUrl = _wechatPayUrl.GetRequestUrl(context.Request.GetType());
                        if (requestUrl == null)
                        {
                            SetPipelineError(context, new ExecuteError($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url"));
                            return;
                        }
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
