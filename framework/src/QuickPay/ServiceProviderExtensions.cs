using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QuickPay.Alipay.Middleware;
using QuickPay.Configurations;
using QuickPay.Exceptions;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.Notify;
using QuickPay.WeChatPay.Middleware;
using System;
namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>配置QuickPay的相关信息
        /// </summary>
        public static IServiceProvider ConfigureQuickPay(this IServiceProvider provider)
        {
            var option = provider.GetService<IOptions<QuickPayConfigurationOption>>().Value;
            var configWrapper = provider.GetService<IOptions<ConfigWrapper>>().Value;

            if (option.ConfigSourceType == ConfigSourceType.FromClass)
            {
                if (configWrapper == null)
                {
                    throw new QuickPayException($"当前配置的类型为:{ConfigSourceType.FromClass},但是'ConfigWrapper' 的配置始终为空!");
                }
            }
            else if (option.ConfigSourceType == ConfigSourceType.FromConfigFile)
            {
                var fileConfigWrapper = ConfigurationFileHelper.TranslateToConfigWrapper(option.ConfigFileName);
                configWrapper.AlipayConfig = fileConfigWrapper.AlipayConfig;
                configWrapper.WeChatPayConfig = fileConfigWrapper.WeChatPayConfig;
            }


            //设置NotifyManager中的Notify
            provider.RegisterNotifies(option);

            //Pipeline
            var pipelineBuilder = provider.GetService<IQuickPayPipelineBuilder>();
            pipelineBuilder
                .UseMiddleware<SetNecessaryMiddleware>()
                .UseMiddleware<AutoUniqueIdMiddleware>()
                .UseMiddleware<AlipayPayDataTransformMiddleware>()
                .UseMiddleware<AlipaySignMiddleware>()
                //.UseMiddleware<AlipayRequestBuilderMiddleware>()

                .UseMiddleware<WeChatPayDataTransformMiddleware>()
                .UseMiddleware<WeChatPaySignMiddleware>()
                //.UseMiddleware<WeChatPayRequestBuilderMiddleware>()

                .UseMiddleware<ExecuterExecuteMiddleware>()
                .UseMiddleware<AlipayParseResponseMiddleware>()
                .UseMiddleware<WeChatPayParseResponseMiddleware>()
                .UseMiddleware<PaymentStoreMiddleware>()
                .UseMiddleware<RefundStoreMiddleware>()
                .UseMiddleware<EndMiddleware>();
            return provider;
        }

        /// <summary>注册通知
        /// </summary>
        private static IServiceProvider RegisterNotifies(this IServiceProvider provider, QuickPayConfigurationOption option)
        {
            var notifyManager = provider.GetService<INotifyManager>();
            foreach (var defination in option.NotifyDefinations)
            {
                notifyManager.AddNotifyByDefination(defination);
            }
            return provider;
        }

    }
}
