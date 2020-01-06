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
        /// <summary>WeChatPayOverrideContextKey
        /// </summary>
        public const string WeChatPayOverrideContextKey = "QuickPay.WeChatPay.Override";

        /// <summary>WeChatPayOverride
        /// </summary>
        protected WeChatPayOverride OverrideValue => WeChatPayOverrideScopeProvider.GetValue(WeChatPayOverrideContextKey);

        /// <summary>WeChatPayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<WeChatPayOverride> WeChatPayOverrideScopeProvider { get; }

        /// <summary>请求执行器
        /// </summary>
        protected IRequestExecuter Executer { get; }

        /// <summary>NotifyTypeFinder
        /// </summary>
        protected INotifyTypeFinder NotifyTypeFinder { get; }

        /// <summary>配置文件存储
        /// </summary>
        protected IWeChatPayConfigStore ConfigStore { get; }

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
            WeChatPayOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<WeChatPayOverride>>();
            Logger = provider.GetService<ILogger<BaseWeChatPayService>>();
            Executer = provider.GetService<IRequestExecuter>();
            NotifyTypeFinder = provider.GetService<INotifyTypeFinder>();
            ConfigStore = provider.GetService<IWeChatPayConfigStore>();
            ObjectMapper = provider.GetService<IObjectMapper>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(string appId)
        {
            var config = ConfigStore.GetByAppId(appId);
            var app = config.GetByAppId(appId);
            return Use(config, app);
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(string configId, string appId)
        {
            var config = ConfigStore.GetConfig(configId);
            var app = config.GetByAppId(appId);
            return Use(config, app);
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(WeChatPayConfig config, WeChatPayApp app)
        {
            var mapConfig = ObjectMapper.Map<WeChatPayConfig>(config);
            var mapApp = ObjectMapper.Map<WeChatPayApp>(app);
            var overrideValue = new WeChatPayOverride(mapConfig, mapApp);
            return WeChatPayOverrideScopeProvider.BeginScope(WeChatPayOverrideContextKey, overrideValue);
        }

        /// <summary>微信支付配置信息
        /// </summary>
        public WeChatPayConfig Config
        {
            get
            {
                if (OverrideValue != null)
                {
                    return OverrideValue.Config;
                }
                throw new ArgumentException($"OverrideValue为空!");
            }
        }

        /// <summary>微信支付应用
        /// </summary>
        public WeChatPayApp App
        {
            get
            {
                if (OverrideValue != null && Config != null)
                {
                    return OverrideValue.App;
                }
                throw new ArgumentException($"OverrideValue为空!");
            }
        }

    }
}
