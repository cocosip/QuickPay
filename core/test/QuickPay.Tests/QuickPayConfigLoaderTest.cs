﻿using Microsoft.Extensions.DependencyInjection;
using QuickPay.Configurations;
using Xunit;

namespace QuickPay.Tests
{
    public class QuickPayConfigLoaderTest : TestBase
    {
        [Fact]
        public void LoadQuickPayConfigTest()
        {

            var configLoader = Provider.GetService<IConfigurationFileTranslator>();
            var configWapper = configLoader.TranslateToConfigWapper("QuickPayConfig.xml", QuickPaySettings.ConfigFormat.Xml);

            var alipayConfig = configWapper.AlipayConfig;
            Assert.Equal("http://127.0.0.1", alipayConfig.NotifyGateway);
            Assert.Equal("127.0.0.1", alipayConfig.LocalAddress);
            Assert.Equal("http://127.0.0.1", alipayConfig.WebGateway);
            Assert.Equal("/Notify/Alipay", alipayConfig.NotifyRealateUrl);
            Assert.Equal("/Notify/Alipay/QrcodeNotify", alipayConfig.QrcodeNotifyRelateUrl);
            Assert.Equal("/Notify/Alipay/BarcodeNotify", alipayConfig.BarcodeNotifyRelateUrl);
            Assert.Equal("https://openapi.alipay.com/gateway.do", alipayConfig.Gateway);
            Assert.Equal("JSON", alipayConfig.Format);
            Assert.Equal("1.0", alipayConfig.Version);
            Assert.Equal("App1", alipayConfig.DefaultAppName);

            var alipayApp = alipayConfig.GetDefaultApp();
            Assert.Equal("2017061307479603", alipayApp.AppId);
            Assert.Equal("App1", alipayApp.Name);
            Assert.Equal(1, alipayApp.AppTypeId);
            Assert.Equal("utf-8", alipayApp.Charset);
            //Assert.Equal("MIIEowIBAAKCAQEAjOeAPWmVTuGFg/QPgrB1XzmqbfiRRcvR+WtFaP2Ul3ndlp7x751xTX40YWH+TnDxBXfF+uo0lPyIQ6toAcMpp/G2ctDupyycEzujYAZZbRDpwlE41psLcL+Vi+Ng1ORD22RSPpRz3Du4L3zX9ntee0NYyYYX4zJ7e3fZT4YBARBHT7TmP6wr8PgV+pb4ihfoZ27LrXoa1z9nMLFvJ/I7cNkgRHYSeTMMhTP3h1sh/Pso/MTY63oOjufOZcNyS4jYDbY+Uv48nNkRsK/FtKnjqZyPKmq3xWtKNy5jOhJjhh9b2hb48BLRjbX7V2IQKa9rjCXj99GSlfNdjhVWeWUclwIDAQABAoIBAFaQt0mDf1ZJ2SQbIhhRXpqVK+6KAn4V3TdVvvvkppB1LzylA9AJMx2/xmB5uqnoWzrXvcsMbieGChVAzhIfG41xQ3zAfY45Kt3qCtIotHH8LRDTo469DEdFfJPHqqrAXiwAM0L9Iz0Pd3W9RlTIsGAcHQUaG7zaO+C73ccsdZt3vSpxVWIbuaX3G6xD6ZXnoOyxtYfs3qpop+wjc9RoIzN+Pqd29jaLYcsN7D72q81gu8k6wpqxMC24O/wy/AhI4fSaYif7XQBAjjOUe93jrxBHcV1Wy0KOSaSHwoQ1nLL5KBVp6RqYb3TGaAKQE3rgpIrJg/x1X3p93nxmt6JnPnkCgYEA6rRB+NIKtOb9S7udL9zXrCV7MnQk7adRkVNAG8OszfADzH5U1HQ11GpnuS/JZu2DeL2lwmzfsk4ef1AcpjHb/iorr9UeGabM1Zp97Ebte9d+LeNdmuNS9DM0l8/R2SGKQ+EFaIGwg5pVuF8NlB8S0CEnabf/IRlj18q27/AKIfUCgYEAmbByZpzVUU+5Z5cB76DTXXbkaEmJhrCUJqWtzhsRy5tc/BimIW4JEzYvM1fWOqGEqhxz/Wo57SttMxsTReCI3lsFtCLhTnxECAZiRuBR4XJpcnVyKonSHWWfUtNiUN95eSkhV4jrcA4BSMTXPzTftxYJFZ5KMaaxte5eMMO30NsCgYB2IYhbDo0pBGJVLfct0gATu0HI4UB9BYw+kyJfVxuxA69FzAgybtNxOKVARlceoUldCkdWFqp4+mzLM61X0RyjTuJyO9hMnPHYSUw8Em8RuCLgQeIpRWXJV8SO7KD4orMO+0FXmn8XniSrCdyxwvobG7TUtzGInVjtkjCFj9HpyQKBgGAE5iSH3Zp0dcBrjvEYiJV/P0qMjxiQX68ZmdIIBYEwqtJxz/FY3uCa3Lh2K0jsOodRSYJNCK3NkOb6BnuEwd4x/glCNYOkjZh57JKdeWqh4ZF6IP7EpnppUDYeDPG7/Reeg889ouKaTWEaYeSCczbe1IQmJfKJU8P3je9niAM7AoGBANhr1tkbmmMN/LzAOYOYxWZeSBHUDvfBIhLGIPtA83uYuHieRIzfcLfUochgJfTbXHfXDgkIrH2Zlfun6XTMobWUoDTK4buaoB9xzXf2XB6u1w9BbuDM/Z4Nkz4vJHwmgXLJ1WnJxz94cocJytatXWiRvErJVNgof3ykb5xkzGov", alipayApp.PrivateKey);
            //Assert.Equal("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwodsg3bVWmNWsm6pPnRRgRGVEBXr5l53HB5W8IZQ1Ff3dOwdiYoNd6rSWGzHGRqyRicf20zc6AEvGcWIjcOEo2Hxh/xCr7AVy9mdIUVPd34/+MDObmvQSlPVcJyYxZveAXyJygw8stcxJnZkZSRxp0NcHJNTpS2kWK23FHakt1qBrWzGwqcE/GGaJ4Awjn3SRab7cO1wwAHUj3MzH3iUiuAJQhIdhTK+VybHf6TFQ71HEn7W5jP2+BCy/fuLcgtVdCLYnGh8JuTuM24inRFP/pxJ0Mimrwqhtp/X2wxUVIu2fGLjao2S0C6/MknEIWH+W4G3AYPUbBqfQeOZ8H4iEwIDAQAB", alipayApp.PublicKey);
            Assert.Equal("RSA2", alipayApp.SignType);
            Assert.False(alipayApp.EnableEncrypt);
            Assert.Equal("AES", alipayApp.EncryptType);
            Assert.Equal("1234567890", alipayApp.EncryptKey);


            var wechatPayConfig = configWapper.WechatPayConfig;
            Assert.Equal("8.8.8.8", wechatPayConfig.LocalAddress);
            Assert.Equal("http://127.0.0.1", wechatPayConfig.WebGateway);
            Assert.Equal("http://127.0.0.1", wechatPayConfig.NotifyGateway);
            Assert.Equal("/Notify/WechatPay", wechatPayConfig.NotifyRealateUrl);
            Assert.Equal("App1", wechatPayConfig.DefaultAppName);
            Assert.Equal("MD5", wechatPayConfig.SignType);
            Assert.Equal("", wechatPayConfig.SslPassword);

            var wechatPayApp = wechatPayConfig.GetDefaultApp();
            Assert.Equal("wx7462799678470f25", wechatPayApp.AppId);
            Assert.Equal("1393813602", wechatPayApp.MchId);
            Assert.Equal("App1", wechatPayApp.Name);
            Assert.Equal(2, wechatPayApp.AppTypeId);
            Assert.Equal("WZEIRIwzeiri1234567890123456789", wechatPayApp.Appsecret);
            Assert.Equal("76bd24f8ec32b4cb21c2db4399caf926", wechatPayApp.Key);
            Assert.Equal("1111111", wechatPayApp.NativeMobileInfo.BundleId);
            Assert.Equal("Ios应用名", wechatPayApp.NativeMobileInfo.IosName);
            Assert.Equal("Android01", wechatPayApp.NativeMobileInfo.AndroidName);
            Assert.Equal("cn.android.0001p", wechatPayApp.NativeMobileInfo.PackageName);
            Assert.Equal("Wap001", wechatPayApp.NativeMobileInfo.WapName);
            Assert.Equal("http://wap.b1.com", wechatPayApp.NativeMobileInfo.WapUrl);
        }
    }
}