using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Runtime;
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
        public BaseAlipayService(IAmbientScopeProvider<AlipayAppOverride> alipayAppOverrideScopeProvider)
        {
            AlipayAppOverrideScopeProvider = alipayAppOverrideScopeProvider;
            Config = IocManager.GetContainer().Resolve<AlipayConfig>();
            Executer = IocManager.GetContainer().Resolve<IRequestExecuter>();
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
           
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
