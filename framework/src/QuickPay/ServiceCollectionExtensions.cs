using DotCommon.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
using QuickPay.Alipay.Utility;
using QuickPay.Assist.Store;
using QuickPay.Configurations;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.Executers;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.Notify;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Authentication;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.Impl;
using QuickPay.WeChatPay.Utility;
using System;
using System.Linq;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加配置
        /// </summary>
        public static IServiceCollection AddQuickPay(this IServiceCollection services, Action<QuickPayConfigurationOption> configure, Action<AlipayConfig> alipayConfigure = null, Action<WeChatPayConfig> weChatPayConfigure = null)
        {

            var option = new QuickPayConfigurationOption();

            //配置Option
            configure(option);

            //从代码中读取
            if (option.ConfigSourceType == ConfigSourceType.FromClass || option.ConfigSourceType == ConfigSourceType.FromConfigFile)
            {
                var alipayConfig = new AlipayConfig();
                var weChatPayConfig = new WeChatPayConfig();
                ConfigWrapper wrapper;
                if (option.ConfigSourceType == ConfigSourceType.FromClass)
                {
                    if (alipayConfigure == null || weChatPayConfigure == null)
                    {
                        throw new QuickPayException($"从代码中加载支付配置时,AlipayConfig与WeChatPayConfig不能为空.");
                    }
                    alipayConfigure(alipayConfig);
                    weChatPayConfigure(weChatPayConfig);
                    wrapper = new ConfigWrapper(alipayConfig, weChatPayConfig);
                }
                else
                {
                    wrapper = ConfigurationFileHelper.TranslateToConfigWrapper(option.ConfigFileName);
                }
                //注册ConfigWrapper
                services
                    .AddSingleton(wrapper)
                    .AddTransient<IAlipayConfigStore, FileAlipayConfigStore>()
                    .AddTransient<IWeChatPayConfigStore, FileWeChatPayConfigStore>();
            }

            //如果从数据库加载多个应用,那么需要自己实现 IAlipayConfigStore,IWeChatPayConfigStore
            //改造后的QuickPay,他可能同时存在多个Config

            services
                .AddSingleton<QuickPayConfigurationOption>(option)
                .RegisterQuickPay()
                .RegisterPipeline()
                .AddAssemblyAutoMaps(typeof(ConfigWrapper).Assembly);
            return services;
        }


        /// <summary>注册QuickPay需要的配置
        /// </summary>
        private static IServiceCollection RegisterQuickPay(this IServiceCollection services)
        {
            //配置信息
            //services.AddSingleton<AlipayConfig>(alipayConfig);
            //services.AddSingleton<WeChatPayConfig>(weChatPayConfig);

            //RequestType Finder
            services.AddSingleton<IRequestTypeFinder, RequestTypeFinder>();
            services.AddSingleton<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();

            services.AddTransient<IExecuteContextFactory, ExecuteContextFactory>();
            services.AddTransient<IRequestExecuter, DefaultRequestExecuter>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            //通知
            services.AddSingleton<INotifyManager, NotifyManager>();
            services.AddSingleton<INotifyTypeFinder, NotifyTypeFinder>();

            //支付数据存储
            services.AddTransient<IPaymentStore, MemoryPaymentStore>();
            services.AddTransient<IRefundStore, MemoryRefundStore>();
            services.AddTransient<IAccessTokenStore, EmptyAccessTokenStore>();
            services.AddTransient<IJsApiTicketStore, EmptyJsApiTicketStore>();

            //支付宝Service
            services.AddTransient<IAlipayAppPayService, AlipayAppPayService>();
            services.AddTransient<IAlipayBarcodePayService, AlipayBarcodePayService>();
            services.AddTransient<IAlipayPagePayService, AlipayPagePayService>();
            services.AddTransient<IAlipayQrcodePayService, AlipayQrcodePayService>();
            services.AddTransient<IAlipayWapPayService, AlipayWapPayService>();
            services.AddTransient<IAlipayTradeCommonService, AlipayTradeCommonService>();
            services.AddTransient<IAlipayAssistService, AlipayAssistService>();
            services.AddTransient<AlipayPayDataHelper>();


            //微信Service

            services.AddTransient<IWeChatAppPayService, WeChatAppPayService>();
            services.AddTransient<IWeChatH5PayService, WeChatH5PayService>();
            services.AddTransient<IWeChatJsApiPayService, WeChatJsApiPayService>();
            services.AddTransient<IWeChatMicroPayService, WeChatMicroPayService>();
            services.AddTransient<IWeChatNativePayService, WeChatNativePayService>();
            services.AddTransient<IWeChatMiniProgramPayService, WeChatMiniProgramPayService>();
            services.AddTransient<IWeChatPayTradeCommonService, WeChatPayTradeCommonService>();
            services.AddTransient<IWeChatPayAssistService, WeChatPayAssistService>();
            services.AddTransient<WeChatPayDataHelper>();

            return services;
        }


        //注册Pipeline
        private static IServiceCollection RegisterPipeline(this IServiceCollection services)
        {
            //查询出全部的中间件
            var middlewareTypies = typeof(QuickPaySettings).Assembly.GetTypes().Where(x => typeof(QuickPayMiddleware).IsAssignableFrom(x) && x != typeof(QuickPayMiddleware));
            foreach (var middlewareType in middlewareTypies)
            {
                services.AddTransient(middlewareType);
            }
            return services;
        }
    }
}
