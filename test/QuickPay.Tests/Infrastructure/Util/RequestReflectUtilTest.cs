using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Util;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using Xunit;

namespace QuickPay.Tests.Infrastructure.Util
{
    public class RequestReflectUtilTest
    {
        [Fact]
        public void ToPayData_Test()
        {
            var request = new JsSdkConfigRequest("JsApiTicket1", "http://127.0.0.1")
            {
                AppId = "123",
                MchId = "MchId1",
                NonceStr = "123456",
                Timestamp = "130001000"
            };
            var payData = RequestReflectUtil.ToPayData(request);
            Assert.Equal("123", payData.GetValue("appId"));
            Assert.Equal("MchId1", payData.GetValue("mch_id"));
            Assert.Equal("123456", payData.GetValue("noncestr"));
            Assert.Equal("JsApiTicket1", payData.GetValue("jsapi_ticket"));
            Assert.Equal("http://127.0.0.1", payData.GetValue("url"));
        }

        [Fact]
        public void ToResponse_Test()
        {
            var payData = new PayData();
            payData.SetValue("appId", "123456");
            payData.SetValue("noncestr", "222222");
            payData.SetValue("timestamp", "130001000");

            var jsSdkConfigResponse = (JsSdkConfigResponse)RequestReflectUtil.ToResponse(payData, typeof(JsSdkConfigResponse));
            Assert.Equal("123456", jsSdkConfigResponse.AppId);
            Assert.Equal("222222", jsSdkConfigResponse.NonceStr);
            Assert.Equal("130001000", jsSdkConfigResponse.Timestamp);
        }
    }
}
