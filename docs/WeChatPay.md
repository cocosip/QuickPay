# QuickPay 组件

## 微信支付

### 微信App支付

- `微信App支付生成预订单`(提交给微信,返回预订单PrepayId等信息)

```c#
public async Task AppUnifiedOrder()
{
    var appPayService = Provider.GetService<IWechatAppPayService>();
    using (appPayService.Use(WechatPayConfig.GetByName("App1")))
    {
        var input = new AppUnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
        AppUnifiedOrderCallResponse response =await appPayService.UnifiedOrder(input);
    }
}
```

### H5网页支付

- `微信H5网页支付`(H5支付下单成功,将返回跳转的url地址,前端重定向到该url)

```c#
public async Task H5UnifiedOrder()
{
    var h5PayService = Provider.GetService<IWechatH5PayService>();
    using (h5PayService.Use(WechatPayConfig.GetByName("App1")))
    {
        var input = new H5UnifiedOrderInput("测试支付1", ObjectId.GenerateNewStringId(), 10);
        string url =await h5PayService.UnifiedOrder(input);
    }
}
```

### 微信公众号支付

>由于公众号支付在支付的时候,必须要有一个OpenId,因此服务端需要先获取用户的OpenId,需要先配置JsSdkConfig给前端使用

- 获取前端所需的JsSdkConfig

```c# 
public async Task GetJsSdkConfig()
{
    var jsApiService = Provider.GetService<IWechatJsApiPayService>();
    using (jsApiService.Use(WechatPayConfig.GetByName("App1")))
    {
        JsSdkConfigResponse response =await jsApiService.GetJsSdkConfig("重定向返回的url(如:http://127.0.0.1)");
    }
}
```

- 从后端获取获取用户Code的微信Url地址

```c#
public  string GetAuthorizationCodeUrl()
{
    var authenticationService = Provider.GetService<IAuthenticationService>();
    var url = authenticationService.GetAuthorizationCodeUrl("AppId", "http://127.0.0.1", state: "STATE1");
    return url;
}
```

- 获取用户OpenId

```c#
public  string GetUserOpenId()
{
    var authenticationService = Provider.GetService<IAuthenticationService>();
    var openId=authenticationService.GetUserOpenIdAsync("AppId","AppSecret","code(上一步返回的code)",state:"STATE1");
    return openId;
}
```

- 公众号提交订单

```c#
public async Task JsApiUnifiedOrder()
{
    var jsApiService = Provider.GetService<IWechatJsApiPayService>();
    using (jsApiService.Use(WechatPayConfig.GetByName("App1")))
    {
        var input = new JsApiUnifiedOrderInput("JsApi支付测试", ObjectId.GenerateNewStringId(), 1, "发起支付的IP地址", "http://127.0.0.1", "上一步获取的OpenId");
        var response =await jsApiService.UnifiedOrder(input)
    }
}
```

### 刷卡支付

- 刷卡支付提交订单

```c#
public void MicroPayUnifiedOrder()
{
    var microPayService = Provider.GetService<IWechatMicroPayService>();
    using (microPayService.Use(WechatPayConfig.GetByName("App1")))
    {
        var input = new MicropayUnifiedOrderInput("刷卡支付1", ObjectId.GenerateNewStringId(), 1, "8.8.8.8", "扫码支付授权码，设备读取用户微信中的条码或者二维码信息");
        var response = await microPayService.UnifiedOrder(input);
    }
}
```