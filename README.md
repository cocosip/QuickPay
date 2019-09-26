# QuickPay 组件

[![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/cocosip/QuickPay/blob/master/LICENSE) ![GitHub last commit](https://img.shields.io/github/last-commit/cocosip/QuickPay.svg) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/cocosip/QuickPay.svg)

| Build Server | Platform | Build Status |
| ------------ | -------- | ------------ |
| Azure Pipelines| Windows |[![Build Status](https://dev.azure.com/cocosip/QuickPay/_apis/build/status/cocosip.QuickPay?branchName=master&jobName=Windows)](https://dev.azure.com/cocosip/QuickPay/_build/latest?definitionId=8&branchName=master)|
| Azure Pipelines| Linux |[![Build Status](https://dev.azure.com/cocosip/QuickPay/_apis/build/status/cocosip.QuickPay?branchName=master&jobName=Linux)](https://dev.azure.com/cocosip/QuickPay/_build/latest?definitionId=8&branchName=master)|

| Package  | Version | Downloads|
| -------- | ------- | -------- |
| `QuickPay` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.svg)](https://www.nuget.org/packages/QuickPay) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.svg)|
| `QuickPay.AspNetCore.Mvc` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.AspNetCore.Mvc.svg)](https://www.nuget.org/packages/QuickPay.AspNetCore.Mvc) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.AspNetCore.Mvc.svg)|
| `QuickPay.SqlServer` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.SqlServer.svg)](https://www.nuget.org/packages/QuickPay.SqlServer) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.SqlServer.svg)|
`QuickPay.Oracle` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.Oracle.svg)](https://www.nuget.org/packages/QuickPay.Oracle) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.Oracle.svg)|

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
public static IServiceProvider Initialize()
{
    IServiceCollection services = new ServiceCollection();
    services.AddLogging(c =>
    {
        c.AddLog4Net();
    })
    .AddCommonComponents()
    .AddDistributedMemoryCache()
    .AddJson4Net()
    .AddWeChatFramework() //添加微信基础框架
    .AddQuickPay(option =>
    {
        option.ConfigSourceType = ConfigSourceType.FromConfigFile;
        option.ConfigFileName = "QuickPayConfig.xml";
        option.ConfigFileFormat=QuickPaySettings.ConfigFormat.Xml;
        option.EnabledAlipaySandbox=false; //是否启用支付宝沙盒
        option.EnabledWechatPaySandbox=false; //是否启用微信沙盒
    })
    .AddQuickPaySqlServer(o =>
    {
        o.DbConnectionString = "...数据库连接字符串...";
        o.PaymentTableName="QP_Payments";
        o.RefundTableName="QP_Refunds";
    });

    Mapper.Initialize(config =>
    {
        config.CreateQuickPayMaps();
    });
    var provider = services.BuildServiceProvider();
    //配置
    provider.ConfigureQuickPay();
    return provider;
}
```

> 配置文件初始化:

- `QuickPay`支持两种方式的配置初始化,1.通过配置的`Xml`([参考](../core/src/QuickPay/QuickPayConfig.xml))或`Josn`([参考](../core/src/QuickPay/QuickPayConfig.json)) 2.通过初始化配置对象

## 示例代码

- [微信](/docs/WeChatPay.md)
- [支付宝](/docs/Alipay.md)