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
using System.Collections.Generic;
using WeChat.Framework;
using DotCommon.AutoMapper;

namespace QuickPay.ConsoleTest
{
    public class Bootstraper
    {
        public static IServiceProvider Initialize()
        {
            var alipayConfig = new AlipayConfig()
            {
                Gateway = "https://openapi.alipay.com/gateway.do",
                NotifyGateway = "http://127.0.0.1",
                NotifyUrlFragments = "/Notify/Alipay",
                BarcodeNotifyUrlFragments = "/Notify/AlipayBarcode",
                QrcodeNotifyUrlFragments = "/Notify/AlipayQrcode",
                LocalAddress = "8.8.8.8",
                WebGateway = "127.0.0.1",
                DefaultAppName = "App1",
                Format = "JSON",
                Version = "1.0",
                Apps = new List<AlipayApp>()
                {
                new AlipayApp("AppName", "AppId", "utf-8", "RSA", "公钥", "私钥", 1, false, "", "")
                }
            };
            var weChatPayConfig = new WeChatPayConfig()
            {
                NotifyGateway = "http://127.0.0.1",
                NotifyUrlFragments = "/Notify/Wxpay",
                LocalAddress = "8.8.8.8",
                WebGateway = "127.0.0.1",
                DefaultAppName = "App1",
                Apps = new List<WeChatPayApp>()
                {
                new WeChatPayApp("AppName", "AppId", "商户号", "加密的Key", "appsecret", 1, new NativeMobileInfo())
                }
            };

            IServiceCollection services = new ServiceCollection();
            services.AddLogging(c =>
                {
                    c.AddLog4Net();
                })
                .AddDotCommon()
                .AddDotCommonAutoMapper()
                .AddJson4Net()
                .AddGenericsMemoryCache()
                .AddWeChatFramework()
                //微信基础框架
                // .AddWeChatFrameworkSqlServer(o =>
                // {
                //     o.DbConnectionString = "数据库连接字符串"
                // }) //微信基础框架SqlServer存储
                .AddQuickPay(option =>
                {
                    option.ConfigSourceType = ConfigSourceType.FromConfigFile;
                    option.ConfigFileName = "QuickPayConfig.xml";
                    option.EnabledAlipaySandbox = false; //是否启用支付宝沙盒
                    option.EnabledWeChatPaySandbox = false; //是否启用微信沙盒
                })
                .AddTransient<WeChatPayDemoService>()
                .AddTransient<AlipayDemoService>()
                //.AddQuickPaySqlServer(o =>
                //{
                //    o.DbConnectionString = "";
                //})
                .BuildAutoMapper();

            var provider = services.BuildServiceProvider();
            //配置
            provider
                .ConfigureDotCommon()
                .ConfigureQuickPay();
            return provider;
        }
    }
}
