using Autofac;
using AutoMapper;
using Castle.Windsor;
using DotCommon.Configurations;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
using System.Collections.Generic;

namespace QuickPay.ConsoleTest
{
    using DotCommonConfiguration = DotCommon.Configurations.Configuration;
    public class Bootstraper
    {
        public static void Initialize()
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

            var builder = new ContainerBuilder();
            var configuration = DotCommonConfiguration.Create()
                 .UseAutofac(builder)
                 .RegisterCommonComponent()
                 .UseJson4Net()
                 .UseLog4Net()
                 .UseMemoryCache()
                 .AddQuickPay("QuickPayConfig.xml")
                 //.AddQuickPay(() => alipayConfig, () => wechatPayConfig)
                 .AutofacBuild()
                 .UseQuickPay();
            //AutoMapper映射
            Mapper.Initialize(config =>
            {
                config.CreateQuickPayMaps();
            });
        }
    }
}
