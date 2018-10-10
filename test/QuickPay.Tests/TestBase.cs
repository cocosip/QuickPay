using AutoMapper;
using DotCommon.Caching;
using DotCommon.DependencyInjection;
using DotCommon.Json4Net;
using DotCommon.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
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
            .AddCommonComponents()
            .AddGenericsMemoryCache()
            .AddJson4Net()
            .AddQuickPay("QuickPayConfig.xml");

            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });
            Provider = services.BuildServiceProvider();
            //配置
            Provider.QuickPayConfigure();

            WechatPayConfig = Provider.GetService<WechatPayConfig>();
            AlipayConfig = Provider.GetService<AlipayConfig>();

        }
    }
}
