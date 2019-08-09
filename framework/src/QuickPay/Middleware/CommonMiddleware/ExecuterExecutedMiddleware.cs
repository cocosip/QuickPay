using Microsoft.Extensions.Logging;
using QuickPay.Alipay;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Http;
using QuickPay.Infrastructure.Requests;
using QuickPay.WeChatPay;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>执行器执行中间件
    /// </summary>
    public class ExecuterExecutedMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IRestClient _alipayRestClient;
        private readonly IRestClient _weChatRestClient;

        /// <summary>Ctor
        /// </summary>
        public ExecuterExecutedMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, QuickPayConfigurationOption option) : base(provider)
        {
            _next = next;
            _alipayRestClient = option.EnabledAlipaySandbox ? new RestClient(AlipaySettings.Urls.Gateway) : new RestClient(AlipaySettings.Urls.SandboxGateway);
            _weChatRestClient = option.EnabledWeChatPaySandbox ? new RestClient(WeChatPaySettings.Urls.SandboxBaseUrl) : new RestClient(WeChatPaySettings.Urls.RealBaseUrl);
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
                    var client = context.Request.Provider == QuickPaySettings.Provider.Alipay ? _alipayRestClient : _weChatRestClient;

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
    }
}
