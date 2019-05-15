using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;

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
            services.AddTransient<ITransferStore, AbpTransferStore>();
            return services;
        }
    }
}
