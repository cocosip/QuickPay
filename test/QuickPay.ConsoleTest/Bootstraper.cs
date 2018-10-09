using AutoMapper;
using DotCommon.DependencyInjection;
using DotCommon.Log4Net;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
using System;
using System.Collections.Generic;

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
                NotifyRealateUrl = "/Notify/Alipay",
                BarcodeNotifyRelateUrl = "/Notify/AlipayBarcode",
                QrcodeNotifyRelateUrl = "/Notify/AlipayQrcode",
                LocalAddress = "8.8.8.8",
                WebGateway = "127.0.0.1",
                DefaultAppName = "App1",
                Format = "JSON",
                Version = "1.0",
                Apps = new List<AlipayApp>(){
                    new AlipayApp("AppName","AppId","utf-8","RSA","公钥","私钥",1,false,"","") }
            };
            var wechatPayConfig = new WechatPayConfig()
            {
                NotifyGateway = "http://127.0.0.1",
                NotifyRealateUrl = "/Notify/Wxpay",
                LocalAddress = "8.8.8.8",
                WebGateway = "127.0.0.1",
                DefaultAppName = "App1",
                Apps = new List<WechatPayApp>()
                {
                    new WechatPayApp("AppName","AppId","商户号","加密的Key","appsecret",1,new NativeMobileInfo())
                }
            };

            IServiceCollection services = new ServiceCollection();
            services.AddLogging(c =>
            {
                c.AddLog4Net();
            }).AddMemoryCache()
            .AddCommonComponents()
            .AddQuickPay(() => alipayConfig, () => wechatPayConfig)
            .AddQuickPay("QuickPayConfig.xml");

            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });
            var provider = services.BuildServiceProvider();
            //配置
            provider.QuickPayConfigure();
            return provider;
        }
    }
}
