using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using System;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加PostgreSql存储
        /// </summary>
        public static IServiceCollection AddQuickPayPostgreSql(this IServiceCollection services, Action<QuickPayPostgreSqlOption> option)
        {
            var quickPaySqlServerOption = new QuickPayPostgreSqlOption();
            option(quickPaySqlServerOption);
            services
                .AddSingleton<QuickPayPostgreSqlOption>(quickPaySqlServerOption)
                .AddTransient<IPaymentStore, PostgreSqlPaymentStore>()
                .AddTransient<IRefundStore, PostgreSqlRefundStore>();
            return services;
        }
    }
}
