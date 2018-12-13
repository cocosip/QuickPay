using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Executers;
using QuickPay.WechatPay.Apps;
using System;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信支付服务基类
    /// </summary>
    public abstract class BaseWechatPayService : IWechatPayService
    {
        /// <summary>WechatPayAppOverrideContextKey
        /// </summary>
        public const string WechatPayAppOverrideContextKey = "Touda.QuickPay.WechatPayApp.Override";

        /// <summary>WechatPayAppOverride
        /// </summary>
        protected WechatPayAppOverride OverrideValue => WechatPayAppOverrideScopeProvider.GetValue(WechatPayAppOverrideContextKey);
        /// <summary>WechatPayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<WechatPayAppOverride> WechatPayAppOverrideScopeProvider { get; }

        /// <summary>微信配置
        /// </summary>
        protected WechatPayConfig Config { get; }

        /// <summary>请求执行器
        /// </summary>
        protected IRequestExecuter Executer { get; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>Ctor
        /// </summary>
        public BaseWechatPayService(IServiceProvider provider)
        {
            WechatPayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<WechatPayAppOverride>>();
            Config = provider.GetService<WechatPayConfig>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();
            Executer = provider.GetService<IRequestExecuter>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(WechatPayApp app)
        {
            var overrideValue = app.ToOverrideValue();
            return WechatPayAppOverrideScopeProvider.BeginScope(WechatPayAppOverrideContextKey, overrideValue);
        }

        /// <summary>微信支付应用
        /// </summary>
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
