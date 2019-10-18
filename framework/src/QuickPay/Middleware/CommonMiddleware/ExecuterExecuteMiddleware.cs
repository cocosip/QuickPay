using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Http;
using QuickPay.Infrastructure.Requests;
using QuickPay.WeChatPay.Apps;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>执行器执行中间件
    /// </summary>
    public class ExecuterExecuteMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private QuickPayConfigurationOption _option;
        private readonly IRestClientFactory _restClientFactory;

        /// <summary>Ctor
        /// </summary>
        public ExecuterExecuteMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, QuickPayConfigurationOption option, IRestClientFactory restClientFactory) : base(provider)
        {
            _next = next;
            _option = option;
            _restClientFactory = restClientFactory;
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
                    //获取当前配置下的请求地址(网关或者/Sandbox)
                    var url = GetUrl(context);
                    var client = _restClientFactory.GetOrAddClient(url);
                    //根据HttpBuilder构建请求
                    var request = context.HttpBuilder.BuildRequest();
                    var response = await client.ExecuteTaskAsync(request);
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


        private string GetUrl(ExecuteContext context)
        {
            string url;
            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                var alipayConfig = (AlipayConfig)context.Config;
                url = _option.EnabledAlipaySandbox ? alipayConfig.SandboxGateway : alipayConfig.Gateway;
            }
            else
            {
                //微信支付
                var weChatPayConfig = (WeChatPayConfig)context.Config;
                url = _option.EnabledWeChatPaySandbox ? weChatPayConfig.SandboxGateway : weChatPayConfig.Gateway;
            }
            return url;
        }

    }
}
