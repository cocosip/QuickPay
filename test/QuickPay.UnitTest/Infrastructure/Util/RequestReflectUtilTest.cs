using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Util;
using QuickPay.WechatPay.Requests;
using Xunit;

namespace QuickPay.UnitTest.Infrastructure.Util
{
    public class RequestReflectUtilTest
    {

        [Fact]
        public void ToPayDataTest()
        {
            var request = new TestRequest("test1", "123456", 1000, "127.0.0.1", "");
            var payData = RequestReflectUtil.ToPayData(request);
            Assert.Equal("test1", payData.GetValue("body"));
            Assert.Equal("123456", payData.GetValue("out_trade_no"));
            Assert.Equal(1000, payData.GetValue("total_fee"));
            Assert.Equal("127.0.0.1", payData.GetValue("spbill_create_ip"));
            Assert.Equal("",payData.GetValue("notify_url"));
        }

        [Fact]
        public void ToResponse()
        {
            var payData = new PayData();
            payData.SetValue("body", "testbody");
            payData.SetValue("out_trade_no", "111222");
            payData.SetValue("total_fee", 100);
            payData.SetValue("spbill_create_ip", "127.0.0.2");
            payData.SetValue("notify_url", "http://127.0.0.1/notify");
            var request = (TestRequest)RequestReflectUtil.ToResponse(payData, typeof(TestRequest));
            Assert.Equal("testbody", request.Body);
            Assert.Equal("111222", request.OutTradeNo);
            Assert.Equal(100, request.TotalFee);
            Assert.Equal("127.0.0.2", request.SpbillCreateIp);
            Assert.Equal("http://127.0.0.1/notify", request.NotifyUrl);
        }


    }
}
