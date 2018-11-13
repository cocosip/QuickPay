using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
using QuickPay.Alipay.Util;
using QuickPay.Configurations;
using QuickPay.Exceptions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Executers;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using QuickPay.WechatPay.Services;
using QuickPay.WechatPay.Services.Impl;
using QuickPay.WechatPay.Util;
using System;
using System.Linq;

namespace QuickPay
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加配置
        /// </summary>
        public static IServiceCollection AddQuickPay(this IServiceCollection services, Action<QuickPayConfigurationOption> option, Action<AlipayConfig> alipayOption = null, Action<WechatPayConfig> wechatPayOption = null)
        {

            var quickPayConfigurationOption = new QuickPayConfigurationOption();
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
                 .RegisterQuickPay(alipayConfig, wechatPayConfig)
                 .RegisterPipeline();
            return services;
        }



        /// <summary>注册QuickPay需要的配置
        /// </summary>
        private static IServiceCollection RegisterQuickPay(this IServiceCollection services, AlipayConfig alipayConfig, WechatPayConfig wechatPayConfig)
        {
            services.AddSingleton<AlipayConfig>(alipayConfig);
            services.AddSingleton<WechatPayConfig>(wechatPayConfig);
            //
            services.AddSingleton<IQuickPayConfigurationFileLoader, QuickPayConfigurationFileLoader>();
            //RequestType Finder
            services.AddSingleton<IRequestTypeFinder, RequestTypeFinder>();
            services.AddSingleton<IQuickPayPipelineBuilder, QuickPayPipelineBuilder>();

            services.AddTransient<IExecuteContextFactory, ExecuteContextFactory>();
            services.AddTransient<IQuickPayConfigManager, QuickPayConfigManager>();
            services.AddTransient<IRequestExecuter, DefaultRequestExecuter>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

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
            services.AddTransient<AlipayPayDataHelper>();


            //微信Service

            services.AddTransient<IWechatAppPayService, WechatAppPayService>();
            services.AddTransient<IWechatH5PayService, WechatH5PayService>();
            services.AddTransient<IWechatJsApiPayService, WechatJsApiPayService>();
            services.AddTransient<IWechatMicroPayService, WechatMicroPayService>();
            services.AddTransient<IWechatNativePayService, WechatNativePayService>();
            services.AddTransient<IWechatMiniProgramPayService, WechatMiniProgramPayService>();
            services.AddTransient<IWechatPayTradeCommonService, WechatPayTradeCommonService>();
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
