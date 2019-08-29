using QuickPay.Alipay.Utility;
using System.Collections.Generic;
using Xunit;

namespace QuickPay.Tests.Alipay.Util
{
    public class AlipayUtilTest
    {
        [Fact]
        public void GenerateTimeStamp_Test()
        {
            var timeStamp = AlipayUtil.GenerateTimeStamp();
            Assert.NotEmpty(timeStamp);
        }

        [Fact]
        public void BuildQuery_Test()
        {
            var dict = new Dictionary<string, object>()
            {
                {"a",1 },
                {"b","2"},
                {"c","" },
                {"d","4" }
            };

            var query = AlipayUtil.BuildQuery(dict, "utf-8");
            Assert.Equal("&a=1&b=2&d=4", query);

        }

        [Fact]
        public void AesEncrypt_Decrypt_Test()
        {
            var txt = "Hello";
            var encryptKey = "MTIzNDU2Nzg5MGFiY2RlZg=="; //1234567890abcdef
            var encryptedTxt = AlipayUtil.AesEncrypt(encryptKey, txt, "utf-8");
            Assert.NotEmpty(encryptedTxt);
            var decryptedTxt = AlipayUtil.AesDecrypt(encryptKey, encryptedTxt, "utf-8");
            Assert.Equal(txt, decryptedTxt);
        }

    }
}
