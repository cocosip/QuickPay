using DotCommon.Json4Net;
using DotCommon.Serializing;
using QuickPay.Alipay.Utility;
using QuickPay.Infrastructure.RequestData;
using Xunit;

namespace QuickPay.Tests.Alipay.Utility
{
    public class AlipayPayDataHelperTest
    {
        //private AlipayPayDataHelper _helper;
        public AlipayPayDataHelperTest()
        {
            //IJsonSerializer jsonSerializer = new NewtonsoftJsonSerializer();
            //_helper = new AlipayPayDataHelper(jsonSerializer);
        }

        //[Fact]
        //public void PayData_JsonAndGetValue_Test()
        //{
        //    var payData = new PayData();
        //    payData.SetValue("appid", "123456");
        //    payData.SetValue("out_trade_no", "aaa");
        //    payData.SetValue("trade_no", "000000");
        //    payData.SetValue("total_amount", 130M);
        //    payData.SetValue("trade_status", "OK");

        //    Assert.Equal("123456", _helper.GetAlipayAppId(payData));
        //    Assert.Equal("000000", _helper.GetAlipayTradeNo(payData));
        //    Assert.Equal(130M, _helper.GetTotalAmount(payData));
        //    Assert.Equal("OK", _helper.GetTradeStatus(payData));

        //    var json = _helper.ToJson(payData);
        //    Assert.NotEmpty(json);
        //    var newPayData = _helper.FromJson(json).GetValues();
        //    foreach (var kv in payData.GetValues())
        //    {
        //        Assert.Equal(kv.Value.ToString(), newPayData[kv.Key].ToString());
        //    }

        //}
    }
}
