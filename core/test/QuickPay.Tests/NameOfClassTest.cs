using Xunit;

namespace QuickPay.Tests
{
    public class NameOfClassTest
    {
        [Fact]
        public void NameOf_Test()
        {
            var actual = nameof(NameOfClassTest);

            Assert.Equal("NameOfClassTest", actual);
        }
    }
}
