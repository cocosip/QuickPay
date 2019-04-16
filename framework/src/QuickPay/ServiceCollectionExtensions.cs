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
using QuickPay.Notify;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Authentication;
using QuickPay.WeChatPay.Services;
using QuickPay.WeChatPay.Services.Impl;
using QuickPay.WeChatPay.Url;
using QuickPay.WeChatPay.Util;
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
        public static IServiceCollection AddQuickPay(this IServiceCollection services, Action<QuickPayConfigurationOption> option, Action<AlipayConfig> alipayOption = null, Action<WeChatPayConfig> weChatPayOption = null)
        {

            var quickPayConfigurationOption = new QuickPayConfigurationOption();

            //配置Option
            option(quickPayConfigurationOption);
            var alipayConfig = new AlipayConfig();
            var weChatPayConfig = new WeChatPayConfig();

            //从代码中读取
            if (quickPayConfigurationOption.ConfigSourceType == ConfigSourceType.FromClass)
            {
                if (alipayOption == null || weChatPayOption == null)
                {
                    throw new QuickPayException($"从代码中加载支付配置时,AlipayConfig与WechatPayConfig不能为空.");
                }
                alipayOption(alipayConfig);
                weChatPayOption(weChatPayConfig);
            }

            services.AddSingleton<QuickPayConfigurationOption>(quickPayConfigurationOption)
                .UseWechatPaySandbox(quickPayConfigurationOption)
                .RegisterQuickPay(alipayConfig, weChatPayConfig)
                .RegisterPipeline();
            return services;
        }

        /// <summary>微信沙盒
        /// </summary>
        private static IServiceCollection UseWechatPaySandbox(this IServiceCollection services, QuickPayConfigurationOption option)
        {
            if (option.EnabledWeChatPaySandbox)
            {
                services.AddSingleton<IWeChatPayUrl, SandboxWeChatPayUrl>();
            }
            else
            {
                services.AddSingleton<IWeChatPayUrl, RealWeChatPayUrl>();
            }
            return services;
        }

        /// <summary>注册QuickPay需要的配置
        /// </summary>
        private static IServiceCollection RegisterQuickPay(this IServiceCollection services, AlipayConfig alipayConfig, WeChatPayConfig weChatPayConfig)
        {

            //配置信息
            services.AddSingleton<AlipayConfig>(alipayConfig);
            services.AddSingleton<WeChatPayConfig>(weChatPayConfig);
            //配置文件读取器
            services.AddSingleton<IConfigurationFileTranslator, ConfigurationFileTranslator>();
            //RequestType Finder
            services.AddSingleton<IRequestTypeFinder, RequestTypeFinder>();
            services.AddSingleton<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();

            services.AddTransient<IExecuteContextFactory, ExecuteContextFactory>();
            services.AddTransient<IQuickPayConfigManager, QuickPayConfigManager>();
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
