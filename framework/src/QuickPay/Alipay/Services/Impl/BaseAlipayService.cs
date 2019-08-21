using DotCommon.ObjectMapping;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Infrastructure.Executers;
using QuickPay.Notify;
using System;
using System.Linq;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝服务基类
    /// </summary>
    public abstract class BaseAlipayService : IAlipayService
    {
        /// <summary>AlipayAppOverrideContextKey
        /// </summary>
        public const string AlipayAppOverrideContextKey = "QuickPay.AlipayApp.Override";

        /// <summary>AlipayAppOverride
        /// </summary>
        protected AlipayAppOverride OverrideValue => AlipayAppOverrideScopeProvider.GetValue(AlipayAppOverrideContextKey);

        /// <summary>AlipayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<AlipayAppOverride> AlipayAppOverrideScopeProvider { get; }

        /// <summary>支付宝配置信息
        /// </summary>
        protected AlipayConfig Config { get; private set; }

        /// <summary>支付宝请求执行器
        /// </summary>
        protected IRequestExecuter Executer { get; }

        /// <summary>NotifyTypeFinder
        /// </summary>
        protected INotifyTypeFinder NotifyTypeFinder { get; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>对象映射
        /// </summary>
        protected IObjectMapper ObjectMapper { get; }

        /// <summary>Ctor
        /// </summary>
        public BaseAlipayService(IServiceProvider provider)
        {
            AlipayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<AlipayAppOverride>>();
            Config = provider.GetService<AlipayConfig>();
            Executer = provider.GetService<IRequestExecuter>();
            NotifyTypeFinder = provider.GetService<INotifyTypeFinder>();
            Logger = provider.GetService<ILoggerFactory>().CreateLogger(QuickPaySettings.LoggerName);
            ObjectMapper = provider.GetService<IObjectMapper>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(AlipayConfig config)
        {
            return Use(config, c => c.Apps.FirstOrDefault());
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(AlipayConfig config, string appName)
        {
            return Use(config, c => c.Apps.FirstOrDefault(x => x.Name == appName));
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(AlipayConfig config, Func<AlipayConfig, AlipayApp> predicate)
        {
            Config = config;
            var overrideValue = predicate(config).ToOverrideValue();
            return AlipayAppOverrideScopeProvider.BeginScope(AlipayAppOverrideContextKey, overrideValue);
        }

        /// <summary>支付宝应用
        /// </summary>
        public AlipayApp App
        {

            get
            {
                if (OverrideValue != null)
                {
                    return OverrideValue.ToAlipayApp();
                }
                var app = Config.GetDefaultApp();
                if (app != null)
                {
                    return app;
                }
                throw new ArgumentException($"AlipayApp为空!");
            }
        }
    }
}
