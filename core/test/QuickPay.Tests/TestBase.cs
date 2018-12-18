using AutoMapper;
using DotCommon.Caching;
using DotCommon.DependencyInjection;
using DotCommon.Json4Net;
using DotCommon.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.WechatPay.Apps;
using System;

namespace QuickPay.Tests
{
    public class TestBase
    {
        protected static IServiceProvider Provider { get; }
        protected static WechatPayConfig WechatPayConfig { get; }
        protected static AlipayConfig AlipayConfig { get; }
        static TestBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(c =>
                {
                    c.AddLog4Net();
                })
                .AddDotCommon()
                .AddGenericsMemoryCache()
                .AddJson4Net()
                .AddQuickPay(option =>
                {
                    option.ConfigSourceType = ConfigSourceType.FromConfigFile;
                    option.ConfigFileName = "QuickPayConfig.xml";
                });

            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });
            Provider = services.BuildServiceProvider();
            //配置
            Provider.ConfigureDotCommon().UseQuickPay();

            WechatPayConfig = Provider.GetService<WechatPayConfig>();
            AlipayConfig = Provider.GetService<AlipayConfig>();

        }
    }
}
