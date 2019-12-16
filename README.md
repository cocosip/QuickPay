# QuickPay 组件

[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu) [![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/cocosip/QuickPay/blob/master/LICENSE) ![GitHub last commit](https://img.shields.io/github/last-commit/cocosip/QuickPay.svg) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/cocosip/QuickPay.svg)

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
`QuickPay.MySql` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.MySql.svg)](https://www.nuget.org/packages/QuickPay.MySql) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.MySql.svg)|
`QuickPay.PostgreSql` | [![NuGet](https://img.shields.io/nuget/v/QuickPay.PostgreSql.svg)](https://www.nuget.org/packages/QuickPay.PostgreSql) |![NuGet](https://img.shields.io/nuget/dt/QuickPay.PostgreSql.svg)|

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
    .AddDotCommon()
    .AddGenericsMemoryCache()
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

- `QuickPay`支持两种方式的配置初始化,1.通过配置的`Xml`([参考](../framework/src/QuickPay/QuickPayConfig.xml)) 2.通过初始化配置对象 3.通过数据库动态加载配置
- 如果通过数据库动态加载配置,需要自定义实现 `IAlipayConfigStore`,`IWeChatPayConfigStore` 配置存储接口,并且需要把他们添加到依赖注入中去。可参考[源码](/framework/src/QuickPay/ServiceCollectionExtensions.cs)

## 示例代码

- [微信](/docs/WeChatPay.md)
- [支付宝](/docs/Alipay.md)
