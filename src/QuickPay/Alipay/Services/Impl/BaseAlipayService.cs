using DotCommon.Logging;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Infrastructure.Executers;
using System;
namespace QuickPay.Alipay.Services.Impl
{
    public abstract class BaseAlipayService : IAlipayService
    {
        public const string AlipayAppOverrideContextKey = "Touda.QuickPay.AlipayApp.Override";
        protected AlipayAppOverride OverrideValue => AlipayAppOverrideScopeProvider.GetValue(AlipayAppOverrideContextKey);
        protected IAmbientScopeProvider<AlipayAppOverride> AlipayAppOverrideScopeProvider { get; }

        protected AlipayConfig Config;
        protected IRequestExecuter Executer { get; }
        protected ILogger Logger { get; }
        public BaseAlipayService(IServiceProvider provider, IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider)
        {
            AlipayAppOverrideScopeProvider = alipayAppOverrideScopeProvider;
            Config = provider.GetService<AlipayConfig>();
            Executer = provider.GetService<IRequestExecuter>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();

        }

        public IDisposable Use(AlipayApp app)
        {
            var overrideValue = app.ToOverrideValue();
            return AlipayAppOverrideScopeProvider.BeginScope(AlipayAppOverrideContextKey, overrideValue);
        }

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
