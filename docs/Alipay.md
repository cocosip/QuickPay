# QuickPay 组件

## 支付宝支付

### 支付宝App支付

- 支付宝App支付
```c#
public async Task TradePay()
{
    var appPayService = Provider.GetService<IAlipayAppPayService>();
    using (appPayService.Use(AlipayConfig.GetByName("App1")))
    {
        var input = new AppTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1");
        var response=await appPayService.TradePay(input);
    }
}
```

### 支付宝PC支付

- 支付宝PC支付,待提交支付信息返回给前端

```c#
public async Task TradePay()
{
    var pagePayService = Provider.GetService<IAlipayPagePayService>();
    using (pagePayService.Use(AlipayConfig.GetByName("App1")))
    {
        var input = new PageTradePayInput("测试1", "支付宝测试支付", ObjectId.GenerateNewStringId(), "0.1")
        {
            ReturnUrl = "http://127.0.0.1/Alipay/ReturnUrl"
        };
        var response = await pagePayService.TradePay(input);
    }
}
```

### 支付宝手机Wap支付

- 支付宝Wap支付

```c#
public async Task TradePay()
{
     var wapPayService = Provider.GetService<IAlipayWapPayService>();
     using (wapPayService.Use(AlipayConfig.GetByName("App1")))
     {
          var input = new WapTradePayInput("手机网站支付1", "支付宝手机网站支付", ObjectId.GenerateNewStringId(), "0.1");
          var response = await wapPayService.TradePay(input);
     }
}
```