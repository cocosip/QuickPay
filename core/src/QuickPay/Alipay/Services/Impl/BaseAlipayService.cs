using DotCommon.Logging;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Infrastructure.Executers;
using System;
namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝服务基类
    /// </summary>
    public abstract class BaseAlipayService : IAlipayService
    {
        /// <summary>AlipayAppOverrideContextKey
        /// </summary>
        public const string AlipayAppOverrideContextKey = "Touda.QuickPay.AlipayApp.Override";
        /// <summary>AlipayAppOverride
        /// </summary>
        protected AlipayAppOverride OverrideValue => AlipayAppOverrideScopeProvider.GetValue(AlipayAppOverrideContextKey);

        /// <summary>AlipayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<AlipayAppOverride> AlipayAppOverrideScopeProvider { get; }

        /// <summary>支付宝配置信息
        /// </summary>
        protected AlipayConfig Config;

        /// <summary>支付宝请求执行器
        /// </summary>
        protected IRequestExecuter Executer { get; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>Ctor
        /// </summary>
        public BaseAlipayService(IServiceProvider provider)
        {
            AlipayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<AlipayAppOverride>>();
            Config = provider.GetService<AlipayConfig>();
            Executer = provider.GetService<IRequestExecuter>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();

        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(AlipayApp app)
        {
            var overrideValue = app.ToOverrideValue();
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
