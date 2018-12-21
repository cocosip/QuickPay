using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
using QuickPay.Alipay.Util;
using QuickPay.Assist.Store;
using QuickPay.Configurations;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Executers;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Services.Impl;
using QuickPay.WechatPay.Url;
using QuickPay.WechatPay.Util;
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
        public static IServiceCollection AddQuickPay(this IServiceCollection services, Action<QuickPayConfigurationOption> option, Action<AlipayConfig> alipayOption = null, Action<WechatPayConfig> wechatPayOption = null)
        {

            var quickPayConfigurationOption = new QuickPayConfigurationOption();
            //添加默认支付宝和微信异步通知
            // quickPayConfigurationOption
            //     .AddNotify<AlipayPaymentNotify>()
            //     .AddNotify<WechatPaymentNotify>();

            //配置Option
            option(quickPayConfigurationOption);
            var alipayConfig = new AlipayConfig();
            var wechatPayConfig = new WechatPayConfig();

            //从代码中读取
            if (quickPayConfigurationOption.ConfigSourceType == ConfigSourceType.FromClass)
            {
                if (alipayOption == null || wechatPayOption == null)
                {
                    throw new QuickPayException($"从代码中加载支付配置时,AlipayConfig与WechatPayConfig不能为空.");
                }
                alipayOption(alipayConfig);
                wechatPayOption(wechatPayConfig);
            }

            services.AddSingleton<QuickPayConfigurationOption>(quickPayConfigurationOption)
                .UseWechatPaySandbox(quickPayConfigurationOption)
                .RegisterQuickPay(alipayConfig, wechatPayConfig)
                .RegisterPipeline();
            return services;
        }

        /// <summary>微信沙盒
        /// </summary>
        private static IServiceCollection UseWechatPaySandbox(this IServiceCollection services, QuickPayConfigurationOption option)
        {
            if (option.EnabledWechatPaySandbox)
            {
                services.AddSingleton<IWechatPayUrl, SandboxWechatPayUrl>();
            }
            else
            {
                services.AddSingleton<IWechatPayUrl, RealWechatPayUrl>();
            }
            return services;
        }

        /// <summary>注册QuickPay需要的配置
        /// </summary>
        private static IServiceCollection RegisterQuickPay(this IServiceCollection services, AlipayConfig alipayConfig, WechatPayConfig wechatPayConfig)
        {

            //配置信息
            services.AddSingleton<AlipayConfig>(alipayConfig);
            services.AddSingleton<WechatPayConfig>(wechatPayConfig);
            //配置文件读取器
            services.AddSingleton<IConfigurationFileTranslator, ConfigurationFileTranslator>();
            //RequestType Finder
            services.AddSingleton<IRequestTypeFinder, RequestTypeFinder>();
            services.AddSingleton<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();

            services.AddTransient<IExecuteContextFactory, ExecuteContextFactory>();
            services.AddTransient<IQuickPayConfigManager, QuickPayConfigManager>();
            services.AddTransient<IRequestExecuter, DefaultRequestExecuter>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            //异步通知管理
            // services.AddSingleton<INotifyManager, NotifyManager>();

            //支付数据存储
            services.AddTransient<IPaymentStore, EmptyPaymentStore>();
            services.AddTransient<IRefundStore, EmptyRefundStore>();
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

            services.AddTransient<IWechatAppPayService, WechatAppPayService>();
            services.AddTransient<IWechatH5PayService, WechatH5PayService>();
            services.AddTransient<IWechatJsApiPayService, WechatJsApiPayService>();
            services.AddTransient<IWechatMicroPayService, WechatMicroPayService>();
            services.AddTransient<IWechatNativePayService, WechatNativePayService>();
            services.AddTransient<IWechatMiniProgramPayService, WechatMiniProgramPayService>();
            services.AddTransient<IWechatPayTradeCommonService, WechatPayTradeCommonService>();
            services.AddTransient<IWechatPayAssistService, WechatPayAssistService>();
            services.AddTransient<WechatPayDataHelper>();

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
