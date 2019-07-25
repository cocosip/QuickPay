using DotCommon.ObjectMapping;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Executers;
using QuickPay.Notify;
using QuickPay.WeChatPay.Apps;
using System;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信支付服务基类
    /// </summary>
    public abstract class BaseWeChatPayService : IWeChatPayService
    {
        /// <summary>WeChatPayAppOverrideContextKey
        /// </summary>
        public const string WeChatPayAppOverrideContextKey = "QuickPay.WeChatPayApp.Override";

        /// <summary>WeChatPayAppOverride
        /// </summary>
        protected WeChatPayAppOverride OverrideValue => WeChatPayAppOverrideScopeProvider.GetValue(WeChatPayAppOverrideContextKey);

        /// <summary>WeChatPayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<WeChatPayAppOverride> WeChatPayAppOverrideScopeProvider { get; }

        /// <summary>微信配置
        /// </summary>
        protected WeChatPayConfig Config { get; }

        /// <summary>请求执行器
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
        public BaseWeChatPayService(IServiceProvider provider)
        {
            WeChatPayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<WeChatPayAppOverride>>();
            Config = provider.GetService<WeChatPayConfig>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();
            Executer = provider.GetService<IRequestExecuter>();
            NotifyTypeFinder = provider.GetService<INotifyTypeFinder>();
            ObjectMapper = provider.GetService<IObjectMapper>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(WeChatPayApp app)
        {
            var overrideValue = app.ToOverrideValue();
            return WeChatPayAppOverrideScopeProvider.BeginScope(WeChatPayAppOverrideContextKey, overrideValue);
        }

        /// <summary>微信支付应用
        /// </summary>
        public WeChatPayApp App
        {

            get
            {
                if (OverrideValue != null)
                {
                    return OverrideValue.ToWeChatPayApp();
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
