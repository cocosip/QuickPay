using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Url;
using QuickPay.WeChatPay.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>执行器执行中间件
    /// </summary>
    public class ExecuterExecuteMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly QuickPayConfigurationOption _option;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WeChatPayDataHelper _weChatPayDataHelper;

        /// <summary>Ctor
        /// </summary>
        public ExecuterExecuteMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, IOptions<QuickPayConfigurationOption> option, IHttpClientFactory httpClientFactory, WeChatPayDataHelper weChatPayDataHelper) : base(provider)
        {
            _next = next;
            _option = option.Value;
            _httpClientFactory = httpClientFactory;
            _weChatPayDataHelper = weChatPayDataHelper;
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
                    var baseUrl = GetUrl(context);
                    var client = _httpClientFactory.CreateClient();
                    var requestMessage = BuildRequestMessage(baseUrl, context);
                    //返回
                    var response = await client.SendAsync(requestMessage);
                    if (!response.IsSuccessStatusCode)
                    {
                        SetPipelineError(context, new ExecuteError($"调用Execute Http返回出错,Http状态:{response.StatusCode}"));
                        return;
                    }
                    //返回数据
                    var responseString = await response.Content.ReadAsStringAsync();
                    //设置返回结果
                    context.HttpResponseString = responseString;

                    Logger.LogInformation(context.Request.GetLogFormat($"执行Execute返回结果:[{responseString}]"));
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


        private HttpRequestMessage BuildRequestMessage(string baseUrl, ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.WeChatPay)
            {
                return BuildWeChatPayRequest(baseUrl, context);
            }
            else
            {
                return BuildAlipayRequest(baseUrl, context);
            }

        }


        private HttpRequestMessage BuildWeChatPayRequest(string baseUrl, ExecuteContext context)
        {
            var requestXml = _weChatPayDataHelper.ToXml(context.RequestPayData);
            var requestResource = WeChatPayUrlHelper.GetRequestResource(context.Request.GetType());
            if (requestResource == null)
            {
                throw new ArgumentException($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url");
            }
            var url = $"{baseUrl}{requestResource}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(requestXml)
            };
            //设置Content-Type
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(QuickPaySettings.ContentTypes.ApplicationXml);
            return requestMessage;
        }

        private HttpRequestMessage BuildAlipayRequest(string baseUrl, ExecuteContext context)
        {
            var p = new SortedDictionary<string, string>();
            foreach (var item in context.RequestPayData.GetValues())
            {
                p.Add(item.Key, item.Value.ToString());
            }
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUrl)
            {
                Content = new FormUrlEncodedContent(p)
            };

            return requestMessage;
        }

    }
}
