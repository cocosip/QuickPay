using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Authentication;
using System;

namespace QuickPay
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuickPayWithAbp(this IServiceCollection services, string file, string format = QuickPaySettings.ConfigFormat.Xml)
        {
            return services.AddQuickPay(file, format)
                  .AddQuickPayAbpExtension();
        }

        public static IServiceCollection AddQuickPayWithAbp(this IServiceCollection services, Func<AlipayConfig> alipayConfig, Func<WechatPayConfig> wechatPayConfig)
        {
            return services.AddQuickPay(alipayConfig, wechatPayConfig)
                .AddQuickPayAbpExtension();
        }

        public static IServiceCollection AddQuickPayAbpExtension(this IServiceCollection services)
        {
            services.AddTransient<IPaymentStore, AbpPaymentStore>();
            services.AddTransient<IRefundStore, AbpRefundStore>();
            services.AddTransient<IAccessTokenStore, AbpAccessTokenStore>();
            services.AddTransient<IJsApiTicketStore, AbpJsApiTicketStore>();
            return services;
        }
    }
}
