using Microsoft.Extensions.DependencyInjection;
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
