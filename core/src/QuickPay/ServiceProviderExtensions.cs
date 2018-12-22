using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Middleware;
using QuickPay.Configurations;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.Notify;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Middleware;
using System;
namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>配置QuickPay的相关信息
        /// </summary>
        public static IServiceProvider UseQuickPay(this IServiceProvider provider)
        {
            var option = provider.GetService<QuickPayConfigurationOption>();
            //从文件中读取配置
            if (option.ConfigSourceType == ConfigSourceType.FromConfigFile)
            {
                var configLoader = provider.GetService<IConfigurationFileTranslator>();
                var alipayConfig = provider.GetService<AlipayConfig>();
                var wechatPayConfig = provider.GetService<WechatPayConfig>();

                var configWapper = configLoader.TranslateToConfigWapper(option.ConfigFileName, option.ConfigFileFormat);
                if (configWapper != null && configWapper.AlipayConfig != null && configWapper.WechatPayConfig != null)
                {
                    alipayConfig.SelfCopy(configWapper.AlipayConfig);
                    wechatPayConfig.SelfCopy(configWapper.WechatPayConfig);
                }
            }

            //设置NotifyManager中的Notify
            provider.RegisterNotifies(option);


            //Pipeline
            var pipelineBuilder = provider.GetService<IQuickPayPipelineBuilder>();
            //设置必要参数
            pipelineBuilder.UseMiddleware<SetNecessaryMiddleware>();
            //自动UniqueId
            pipelineBuilder.UseMiddleware<AutoUniqueIdMiddleware>();

            //支付宝
            //Request转PayData
            pipelineBuilder.UseMiddleware<AlipayPayDataTransformMiddleware>();
            //签名
            pipelineBuilder.UseMiddleware<AlipaySignMiddleware>();
            //构建RequestBuilder
            pipelineBuilder.UseMiddleware<AlipayRequestBuilderMiddleware>();

            //微信
            //Request转PayData
            pipelineBuilder.UseMiddleware<WechatPayDataTransformMiddleware>();
            //签名
            pipelineBuilder.UseMiddleware<WechatPaySignMiddleware>();
            //构建RequestBuilder
            pipelineBuilder.UseMiddleware<WechatPayRequestBuilderMiddleware>();

            //执行Execute
            pipelineBuilder.UseMiddleware<ExecuterExecutedMiddleware>();

            //数据返回格式化
            pipelineBuilder.UseMiddleware<AlipayParseResponseMiddleware>();
            pipelineBuilder.UseMiddleware<WechatPayParseResponseMiddleware>();
            //支付存储
            pipelineBuilder.UseMiddleware<PaymentStoreMiddleware>();
            //退款存储
            pipelineBuilder.UseMiddleware<RefundStoreMiddleware>();
            //结束
            pipelineBuilder.UseMiddleware<EndMiddleware>();

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
