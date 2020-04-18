using AutoMapper;
using DotCommon.Caching;
using DotCommon.DependencyInjection;
using DotCommon.Json4Net;
using DotCommon.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.Configurations;
using QuickPay.WeChatPay.Apps;
using System;
using WeChat.Framework;
using DotCommon.AutoMapper;
using Microsoft.Extensions.Options;

namespace QuickPay.Tests
{
    public class TestBase
    {
        protected static IServiceProvider Provider { get; }
        protected static ConfigWrapper Wrapper { get; }
        static TestBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(c =>
                {
                    c.AddLog4Net();
                })
                .AddDotCommon()
                .AddDotCommonAutoMapper()
                .AddGenericsMemoryCache()
                .AddJson4Net()
                .AddHttpClient()
                .AddWeChatFramework() //微信基础框架使用内存存储AccessToken与JsTicket
                .AddQuickPay(option =>
                {
                    option.ConfigSourceType = ConfigSourceType.FromConfigFile;
                    option.ConfigFileName = "QuickPayConfig.xml";
                })
                .BuildAutoMapper();
            Provider = services.BuildServiceProvider();
            //配置
            Provider
                .ConfigureDotCommon()
                .ConfigureQuickPay();

            Wrapper = Provider.GetService<IOptions<ConfigWrapper>>().Value;

        }
    }
}
