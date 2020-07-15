using DotCommon.Serializing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace QuickPay.WeChat.Frame.Service
{
    /// <summary>抽象Service基类
    /// </summary>
    public abstract class WeChatFrameServiceBase
    {
        /// <summary>Provider
        /// </summary>
        protected IServiceProvider Provider { get; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>Json格式消息转化
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }

        /// <summary>HttpClientFactory
        /// </summary>
        protected IHttpClientFactory HttpClientFactory { get; }

        /// <summary>Ctor
        /// </summary>
        protected WeChatFrameServiceBase(IServiceProvider provider, ILogger<WeChatFrameServiceBase> logger)
        {
            Provider = provider;
            Logger = logger;
            JsonSerializer = provider.GetService<IJsonSerializer>();
            HttpClientFactory = provider.GetService<IHttpClientFactory>();
        }

        /// <summary>格式化日志
        /// </summary>
        protected string ParseLog(string appId, string methodName, string content)
        {
            return string.Format("微信AppId:{0},调用方法:{1}。{2}", appId, methodName, content);
        }

    }
}
