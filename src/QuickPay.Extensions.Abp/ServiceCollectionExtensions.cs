using Microsoft.Extensions.DependencyInjection;
using QuickPay.PayAux.Store;
using QuickPay.WechatPay.Authentication;

namespace QuickPay
{
    public static class ServiceCollectionExtensions
    {
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
