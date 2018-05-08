using DotCommon.Reflecting;
using QuickPay.Infrastructure.RequestData;
using QuickPay.WechatPay.Requests;
using System.Reflection;
using Xunit;

namespace QuickPay.UnitTest.Infrastructure.RequestData
{
    public class PayElementTest
    {
        [Fact]
        public void OverridePayElementTest()
        {
            var request = new AppUnifiedOrderCallRequest("123456");
            request.SetNecessary(new WechatPay.Apps.WechatPayConfig(), new WechatPay.Apps.WechatPayApp());

            PayElementAttribute noncestrAttr = null;
            var properties = PropertyInfoUtil.GetProperties(request.GetType());
            foreach (var property in properties)
            {
                //获取该属性的Attribute
                var attribute = property.GetCustomAttribute<PayElementAttribute>();
                if (attribute != null && attribute.Name == "noncestr")
                {
                    noncestrAttr = attribute;
                }
            }

            Assert.True(noncestrAttr != null);
            Assert.Equal("noncestr", noncestrAttr.Name);
        }

    }
}
