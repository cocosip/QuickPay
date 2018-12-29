using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Assist.Store;
using QuickPay.Configurations;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Authentication;
using System;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加QuickPay Abp扩展
        /// </summary>
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
