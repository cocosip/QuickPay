﻿using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Services;
using QuickPay.Alipay.Services.Impl;
using QuickPay.Alipay.Util;
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
        public static IServiceCollection AddQuickPay(this IServiceCollection services, string file, string format = QuickPaySettings.ConfigFormat.Xml)
        {
            services.AddSingleton<QuickPayConfigFile>(new QuickPayConfigFile()
            {
                FileName = file,
                Format = format
            });
            RegisterQuickPay(services, new AlipayConfig(), new WechatPayConfig());
            RegisterPipeline(services);
            return services;
        }

        public static IServiceCollection AddQuickPay(this IServiceCollection services, Func<AlipayConfig> alipayConfig, Func<WechatPayConfig> wechatPayConfig)
        {
            services.AddSingleton<QuickPayConfigFile>(new QuickPayConfigFile()
            {
                FileName = "",
                Format = ""
            })
            .RegisterQuickPay(new AlipayConfig(), new WechatPayConfig())
            .RegisterPipeline();
            return services;
        }


        private static IServiceCollection RegisterQuickPay(this IServiceCollection services, AlipayConfig alipayConfig, WechatPayConfig wechatPayConfig)
        {
            services.AddSingleton<AlipayConfig>(alipayConfig);
            services.AddSingleton<WechatPayConfig>(wechatPayConfig);
            //RequestType Finder
            services.AddSingleton<IRequestTypeFinder, RequestTypeFinder>();
            services.AddSingleton<QuickPayConfigLoader>();
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

            var assemply = typeof(QuickPaySettings).Assembly;
            //var assemply = Assembly.Load(AssemblyName.GetAssemblyName(QuickPaySettings.AssemblyName));
            var middlewareTypies = assemply.GetTypes().Where(x => typeof(QuickPayMiddleware).IsAssignableFrom(x) && x != typeof(QuickPayMiddleware));
            foreach (var middlewareType in middlewareTypies)
            {
                services.AddTransient(middlewareType);
                //Console.WriteLine(middlewareType.Name);
            }
            return services;
        }
    }
}