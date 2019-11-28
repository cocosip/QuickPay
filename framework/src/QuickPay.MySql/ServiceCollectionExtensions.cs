using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using System;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加MySql存储
        /// </summary>
        public static IServiceCollection AddQuickPayMySql(this IServiceCollection services, Action<QuickPayMySqlOption> option)
        {
            var quickPaySqlServerOption = new QuickPayMySqlOption();
            option(quickPaySqlServerOption);
            services
                .AddSingleton<QuickPayMySqlOption>(quickPaySqlServerOption)
                .AddTransient<IPaymentStore, MySqlPaymentStore>()
                .AddTransient<IRefundStore, MySqlRefundStore>();
            return services;
        }
    }
}
