using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Runtime;
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
        public BaseWechatPayService(IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider)
        {
            WechatPayAppOverrideScopeProvider = wechatPayAppOverrideScopeProvider;
            Config = IocManager.GetContainer().Resolve<WechatPayConfig>();
            Executer = IocManager.GetContainer().Resolve<IRequestExecuter>();
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
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
                throw new ArgumentException($"WxpayApp为空!");
            }
        }




    }
}
