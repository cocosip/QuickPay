using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using QuickPay.WechatPay;
using QuickPay.WechatPay.Authentication;

namespace QuickPay
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuickPaySqlServer(this IServiceCollection services)
        {
            services.AddTransient<IPaymentStore, SqlServerPaymentStore>();
            services.AddTransient<IRefundStore, SqlServerRefundStore>();
            services.AddTransient<IAccessTokenStore, SqlServerAccessTokenStore>();
            services.AddTransient<IJsApiTicketStore, SqlServerJsApiTicketStore>();
            return services;
        }
    }
}
