using AutoMapper;
using DotCommon.DependencyInjection;
using DotCommon.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace QuickPay.Tests
{
    public class TestBase
    {
        protected static IServiceProvider Provider { get; }
        static TestBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(c =>
            {
                c.AddLog4Net();
            }).AddMemoryCache()
            .AddCommonComponents()
            .AddQuickPay("QuickPayConfig.xml");

            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });
            var provider = services.BuildServiceProvider();
            //配置
            provider.QuickPayConfigure();
            Provider = provider;

        }
    }
}
