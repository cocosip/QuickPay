using DotCommon.ObjectMapping;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Infrastructure.Executers;
using QuickPay.Notify;
using System;

namespace QuickPay.Alipay.Services.Impl
{
    /// <summary>支付宝服务基类
    /// </summary>
    public abstract class BaseAlipayService : IAlipayService
    {
        /// <summary>AlipayOverrideContextKey
        /// </summary>
        public const string AlipayOverrideContextKey = "QuickPay.Alipay.Override";

        /// <summary>AlipayOverride
        /// </summary>
        protected AlipayOverride OverrideValue => AlipayOverrideScopeProvider.GetValue(AlipayOverrideContextKey);

        /// <summary>AlipayAppOverrideScopeProvider
        /// </summary>
        protected IAmbientScopeProvider<AlipayOverride> AlipayOverrideScopeProvider { get; }

        /// <summary>支付宝请求执行器
        /// </summary>
        protected IRequestExecuter Executer { get; }

        /// <summary>NotifyTypeFinder
        /// </summary>
        protected INotifyTypeFinder NotifyTypeFinder { get; }

        /// <summary>配置文件存储
        /// </summary>
        protected IAlipayConfigStore ConfigStore { get; }

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
            AlipayOverrideScopeProvider = provider.GetService<IAmbientScopeProvider<AlipayOverride>>();
            Executer = provider.GetService<IRequestExecuter>();
            NotifyTypeFinder = provider.GetService<INotifyTypeFinder>();
            ConfigStore = provider.GetService<IAlipayConfigStore>();
            Logger = provider.GetService<ILogger<BaseAlipayService>>();
            ObjectMapper = provider.GetService<IObjectMapper>();
        }

        /// <summary>Use
        /// </summary>
        public IDisposable Use(string appId)
        {
            var config = ConfigStore.GetConfigByAppId(appId);
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
        public IDisposable Use(AlipayConfig config, AlipayApp app)
        {
            var mapConfig = ObjectMapper.Map<AlipayConfig>(config);
            var mapApp = ObjectMapper.Map<AlipayApp>(app);
            var overrideValue = new AlipayOverride(mapConfig, mapApp);
            return AlipayOverrideScopeProvider.BeginScope(AlipayOverrideContextKey, overrideValue);
        }


        /// <summary>支付宝配置信息
        /// </summary>
        public AlipayConfig Config
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

        /// <summary>支付宝应用
        /// </summary>
        public AlipayApp App
        {
            get
            {
                if (OverrideValue != null)
                {
                    return OverrideValue.App;
                }
                throw new ArgumentException($"OverrideValue为空!");
            }
        }
    }
}
