using QuickPay.Alipay.Apps;
using Xunit;

namespace QuickPay.Tests.Alipay.Apps
{
    public class AlipayAppAndAlipayAppOverrideTest
    {
        [Fact]
        public void AlipayApp_ToOverride_Test()
        {
            var alipayApp = new AlipayApp()
            {
                Name = "alipay1",
                AppId = "alipay-appid1",
                AppTypeId = 1,
                Charset = "utf-8",
                SignType = "RSA",
                PublicKey = "123",
                PrivateKey = "123",
                EnableEncrypt = true,
                EncryptKey = "123456",
                EncryptType = "AES"
            };
            var appOverride = alipayApp.ToOverrideValue();
            Assert.Equal(alipayApp.AppId, appOverride.AppId);
            Assert.Equal(alipayApp.Name, appOverride.Name);
            Assert.Equal(alipayApp.AppTypeId, appOverride.AppTypeId);
            Assert.Equal(alipayApp.Charset, appOverride.Charset);
            Assert.Equal(alipayApp.SignType, appOverride.SignType);
            Assert.Equal(alipayApp.PublicKey, appOverride.PublicKey);
            Assert.Equal(alipayApp.PrivateKey, appOverride.PrivateKey);
            Assert.Equal(alipayApp.EnableEncrypt, appOverride.EnableEncrypt);
            Assert.Equal(alipayApp.EncryptKey, appOverride.EncryptKey);
            Assert.Equal(alipayApp.EncryptType, appOverride.EncryptType);
        }
    }
}
