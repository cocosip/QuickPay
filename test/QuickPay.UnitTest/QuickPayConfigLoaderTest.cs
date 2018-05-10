using DotCommon.Serializing;
using Xunit;

namespace QuickPay.UnitTest
{
    public class QuickPayConfigLoaderTest
    {
        [Fact]
        public void LoadQuickPayConfigTest()
        {
            IJsonSerializer jsonSerializer = new NewtonsoftJsonSerializer();
            var configLoader = new QuickPayConfigLoader(jsonSerializer);
            var configWapper = configLoader.LoadConfigWapper("ConfigTest.xml", QuickPaySettings.ConfigFormat.Xml);

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
            Assert.Equal("AppId1", alipayApp.AppId);
            Assert.Equal("App1", alipayApp.Name);
            Assert.Equal(1, alipayApp.AppTypeId);
            Assert.Equal("utf-8", alipayApp.Charset);
            Assert.Equal("私钥", alipayApp.PrivateKey);
            Assert.Equal("公钥", alipayApp.PublicKey);
            Assert.Equal("RSA", alipayApp.SignType);
            Assert.False(alipayApp.EnableEncrypt);
            Assert.Equal("AES", alipayApp.EncryptType);
            Assert.Equal("123", alipayApp.EncryptKey);


            var wechatPayConfig = configWapper.WechatPayConfig;
            Assert.Equal("8.8.8.8", wechatPayConfig.LocalAddress);
            Assert.Equal("http://127.0.0.1", wechatPayConfig.WebGateway);
            Assert.Equal("http://127.0.0.1", wechatPayConfig.NotifyGateway);
            Assert.Equal("/Notify/WechatPay", wechatPayConfig.NotifyRealateUrl);
            Assert.Equal("App1", wechatPayConfig.DefaultAppName);
            Assert.Equal("MD5", wechatPayConfig.SignType);
            Assert.Equal("", wechatPayConfig.SslPassword);

            var wechatPayApp = wechatPayConfig.GetDefaultApp();
            Assert.Equal("AppId", wechatPayApp.AppId);
            Assert.Equal("MchId", wechatPayApp.MchId);
            Assert.Equal("App1", wechatPayApp.Name);
            Assert.Equal(2, wechatPayApp.AppTypeId);
            Assert.Equal("appsecret", wechatPayApp.Appsecret);
            Assert.Equal("密钥", wechatPayApp.Key);
            Assert.Equal("1111111", wechatPayApp.NativeMobileInfo.BundleId);
            Assert.Equal("Ios应用名", wechatPayApp.NativeMobileInfo.IosName);
            Assert.Equal("Android01", wechatPayApp.NativeMobileInfo.AndroidName);
            Assert.Equal("cn.android.0001p", wechatPayApp.NativeMobileInfo.PackageName);
            Assert.Equal("Wap001", wechatPayApp.NativeMobileInfo.WapName);
            Assert.Equal("http://wap.b1.com", wechatPayApp.NativeMobileInfo.WapUrl);
        }
    }
}
