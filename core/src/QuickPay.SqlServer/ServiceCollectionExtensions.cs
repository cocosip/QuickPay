﻿using Microsoft.Extensions.DependencyInjection;
using QuickPay.Assist.Store;
using QuickPay.WechatPay;
using QuickPay.WechatPay.Authentication;
using System;

namespace QuickPay
{
    /// <summary>依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加SqlServer存储
        /// </summary>
        public static IServiceCollection AddQuickPaySqlServer(this IServiceCollection services, Action<QuickPaySqlServerOption> option)
        {
            var quickPaySqlServerOption = new QuickPaySqlServerOption();
            option(quickPaySqlServerOption);
            services.AddSingleton<QuickPaySqlServerOption>(quickPaySqlServerOption);

            services.AddTransient<IPaymentStore, SqlServerPaymentStore>();
            services.AddTransient<IRefundStore, SqlServerRefundStore>();
            services.AddTransient<IAccessTokenStore, SqlServerAccessTokenStore>();
            services.AddTransient<IJsApiTicketStore, SqlServerJsApiTicketStore>();
            return services;
        }
    }
}
