# QuickPay 组件

## 简介

- `QuickPay` 集成了微信,支付宝支付,能够通过简单的配置,简短的代码接入支付功能。
- 微信支持功能: `App支付` , `H5支付` , `JsApi(公众号)支付` , `扫码支付` , `刷卡支付`。
- 支付宝支持功能: `App支付` , `电脑网站支付` , `扫码支付` , `手机网站(H5)支付`
- 支持同一个网站下多个应用支付。

## 接入

- `QuickPay` 组件依赖 [DotCommon](https://github.com/cocosip/DotCommon)通用类库与部分相关的组件。因此初始化需要初始化 `DotCommon` 类库。

- Nuget依赖包:`Install-Package QuickPay`

> 初始化代码:

```c#
public static void Initialize()
{
    var builder = new ContainerBuilder();
    var configuration = DotCommonConfiguration.Create()
            .UseAutofac(builder)
            .RegisterCommonComponent()
            .UseJson4Net()
            .UseLog4Net()
            .UseMemoryCache()
            .AddQuickPay("QuickPayConfig.xml")
            .AutofacBuild()
            .UseQuickPay();
    //AutoMapper映射
    Mapper.Initialize(config =>
    {
        config.CreateQuickPayMaps();
    });
}
```

> 配置文件初始化:
- `QuickPay`支持两种方式的配置初始化,1.通过配置的`Xml`([参考](../src/QuickPay/ConfigDemo.xml))或`Josn`([参考](../src/QuickPay/ConfigDemo.json)) 2.通过初始化配置对象

```c#
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
        Apps = new List<AlipayApp>()
        {
            new AlipayApp("AppName","AppId","utf-8","RSA","公钥","私钥",1,false,"","")
        }
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
        .AddQuickPay(() => alipayConfig, () => wechatPayConfig)
        .AutofacBuild()
        .UseQuickPay();
    //AutoMapper映射
    Mapper.Initialize(config =>
    {
        config.CreateQuickPayMaps();
    });
}

```
