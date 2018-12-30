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
        /// <summary>WechatPayAppOverrideContextKey
        /// </summary>
        public const string WechatPayAppOverrideContextKey = "Touda.QuickPay.WechatPayApp.Override";

        /// <summary>WechatPayAppOverride
        /// </summary>
        protected WeChatPayAppOverride OverrideValue => WechatPayAppOverrideScopeProvider.GetValue(WechatPayAppOverrideContextKey);
        /// <summary>WechatPayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<WeChatPayAppOverride> WechatPayAppOverrideScopeProvider { get; }

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


        /// <summary>Ctor
        /// </summary>
        public BaseWeChatPayService(IServiceProvider provider)
        {
            WechatPayAppOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<WeChatPayAppOverride>>();
            Config = provider.GetService<WeChatPayConfig>();
            Logger = provider.GetService<ILogger<QuickPayLoggerName>>();
            Executer = provider.GetService<IRequestExecuter>();
            NotifyTypeFinder = provider.GetService<INotifyTypeFinder>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(WeChatPayApp app)
        {
            var overrideValue = app.ToOverrideValue();
            return WechatPayAppOverrideScopeProvider.BeginScope(WechatPayAppOverrideContextKey, overrideValue);
        }

        /// <summary>微信支付应用
        /// </summary>
        public WeChatPayApp App
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
