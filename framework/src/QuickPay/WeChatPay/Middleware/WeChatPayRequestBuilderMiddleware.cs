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
        private readonly WeChatPayDataHelper _wechatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WeChatPayRequestBuilderMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, QuickPayConfigurationOption option, WeChatPayDataHelper wechatPayDataHelper) : base(provider)
        {
            _next = next;
            _option = option;
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
                        var requestResource = WeChatPayUrlHelper.GetRequestResource(context.Request.GetType());
                        if (requestResource == null)
                        {
                            SetPipelineError(context, new ExecuteError($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url"));
                            return;
                        }

                        HttpBuilder builder = new HttpBuilder(requestResource, Method.POST, DataFormat.Xml);
                        builder.AddParameter(new Parameter("", requestXml, ParameterType.RequestBody));
                        context.HttpBuilder = builder;
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
