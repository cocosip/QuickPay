using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Executers;
using QuickPay.WechatPay.Apps;
using System;

namespace QuickPay.WechatPay.Services.Impl
{
    public abstract class BaseWechatPayService : IWechatPayService
    {
        public const string WechatPayAppOverrideContextKey = "Touda.QuickPay.WechatPayApp.Override";
        protected WechatPayAppOverride OverrideValue => WechatPayAppOverrideScopeProvider.GetValue(WechatPayAppOverrideContextKey);
        protected IAmbientScopeProvider<WechatPayAppOverride> WechatPayAppOverrideScopeProvider { get; }
        protected WechatPayConfig Config { get; }
        protected IRequestExecuter Executer { get; }
        protected ILogger Logger { get; }
        public BaseWechatPayService(IServiceProvider provider)
        {
            WechatPayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<WechatPayAppOverride>>();
            Config = provider.GetService<WechatPayConfig>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();
            Executer = provider.GetService<IRequestExecuter>();
        }
        public IDisposable Use(WechatPayApp app)
        {
            var overrideValue = app.ToOverrideValue();
            return WechatPayAppOverrideScopeProvider.BeginScope(WechatPayAppOverrideContextKey, overrideValue);
        }

        public WechatPayApp App
        {

            get
            {
                if (OverrideValue != null)
                {
                    return OverrideValue.ToWechatPayApp();
                }
                var app = Config.GetDefaultApp();
                if (app != null)
                {
                    return app;
                }
                throw new ArgumentException($"WechatPayApp为空!");
            }
        }




    }
}
