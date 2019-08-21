using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Middleware;
using QuickPay.Configurations;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.Notify;
using QuickPay.WeChatPay.Apps;
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
            var option = provider.GetService<QuickPayConfigurationOption>();
            //从文件中读取配置
            if (option.ConfigSourceType == ConfigSourceType.FromConfigFile)
            {
                var alipayConfig = provider.GetService<AlipayConfig>();
                var weChatPayConfig = provider.GetService<WeChatPayConfig>();
                var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper(option.ConfigFileName);

                if (wrapper != null && wrapper.AlipayConfig != null && wrapper.WeChatPayConfig != null)
                {
                    alipayConfig.SelfCopy(wrapper.AlipayConfig);
                    weChatPayConfig.SelfCopy(wrapper.WeChatPayConfig);
                }
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
                .UseMiddleware<AlipayRequestBuilderMiddleware>()

                .UseMiddleware<WeChatPayDataTransformMiddleware>()
                .UseMiddleware<WeChatPaySignMiddleware>()
                .UseMiddleware<WeChatPayRequestBuilderMiddleware>()

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
