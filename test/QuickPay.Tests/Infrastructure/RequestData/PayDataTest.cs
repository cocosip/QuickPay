using QuickPay.Infrastructure.RequestData;
using Xunit;

namespace QuickPay.Tests.Infrastructure.RequestData
{
    public class PayDataTest
    {
        [Fact]
        public void PayData_GetAndSet_Test()
        {
            var payData = new PayData();
            payData.SetValue("id", 123);
            payData.SetValue("money", 15.02M);

            Assert.Equal(123, payData.GetValue("id"));
            Assert.Equal(15.02M, payData.GetValue("money"));
            Assert.True(payData.IsSet("id"));

            var dict = payData.GetValues();

            Assert.Equal(123, dict["id"]);
            Assert.Equal(15.02M, dict["money"]);

        }
    }
}
