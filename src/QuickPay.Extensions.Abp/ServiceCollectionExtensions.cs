﻿using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.Assist.Store;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using System;

namespace QuickPay
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuickPayWithAbp(this IServiceCollection services, Action<QuickPayConfigurationOption> option, Action<AlipayConfig> alipayOption = null, Action<WechatPayConfig> wechatPayOption = null)
        {
            return services.AddQuickPay(option, alipayOption, wechatPayOption)
                  .AddQuickPayAbp();
        }


        public static IServiceCollection AddQuickPayAbp(this IServiceCollection services)
        {
            services.AddTransient<IPaymentStore, AbpPaymentStore>();
            services.AddTransient<IRefundStore, AbpRefundStore>();
            services.AddTransient<IAccessTokenStore, AbpAccessTokenStore>();
            services.AddTransient<IJsApiTicketStore, AbpJsApiTicketStore>();
            return services;
        }
    }
}
