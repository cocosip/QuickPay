﻿using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using System;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加Oracle存储
        /// </summary>
        public static IServiceCollection AddQuickPayOracle(this IServiceCollection services, Action<QuickPayOracleOption> option)
        {
            var quickPaySqlServerOption = new QuickPayOracleOption();
            option(quickPaySqlServerOption);
            services
                .AddSingleton<QuickPayOracleOption>(quickPaySqlServerOption)
                .AddTransient<IPaymentStore, OraclePaymentStore>()
                .AddTransient<IRefundStore, OracleRefundStore>();
            return services;
        }
    }
}
