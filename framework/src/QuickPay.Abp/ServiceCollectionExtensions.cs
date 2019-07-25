using DotCommon.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist;
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
            services
                .AddTransient<IPaymentStore, AbpPaymentStore>()
                .AddTransient<IRefundStore, AbpRefundStore>()
                .AddTransient<ITransferStore, AbpTransferStore>()
                .AddAssemblyAutoMaps(typeof(AbpPayment).Assembly);
            return services;
        }
    }
}
