using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Requests;
using QuickPay.Middleware;
using System;
using System.Linq;
using Xunit;

namespace QuickPay.Tests.Middleware
{
    public class AutoUniqueIdMiddlewareTest
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;
        public AutoUniqueIdMiddlewareTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            _provider = services.BuildServiceProvider();

            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();

        }


        [Fact]
        public void Invoke_RequestIsNull_Test()
        {
            var autoUniqueIdMiddleware = new AutoUniqueIdMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);
            var context = new ExecuteContext()
            {
                Request = null,
                App = new AlipayApp()
            };
            autoUniqueIdMiddleware.Invoke(context).Wait();
            Assert.True(context.Errors.Any());
            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);
        }

        [Fact]
        public void Invoke_SetUniqueId_BusinessCode_Test()
        {
            var autoUniqueIdMiddleware = new AutoUniqueIdMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);
            var context = new ExecuteContext()
            {
                Request = new QuickPay.Alipay.Requests.AppTradePayRequest()
            };
            context.Request.BusinessCode = "";
            autoUniqueIdMiddleware.Invoke(context).Wait();
            Assert.Equal("Default", context.Request.BusinessCode);
            Assert.NotNull(context.Request.UniqueId);
        }

        [Fact]
        public void Invoke_BizContentRequestIsNull_Test()
        {
            var autoUniqueIdMiddleware = new AutoUniqueIdMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);

            var request = new AppTradePayRequest();
            request.SetBizContentRequest(new AppTradeBizContentPayRequest());
            var context = new ExecuteContext()
            {
                Request = request
            };

            Assert.Equal(QuickPaySettings.Provider.Alipay, context.Request.Provider);
            autoUniqueIdMiddleware.Invoke(context).Wait();
            //Assert.True(context.Errors.Any());
            //_mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);

        }

    }
}
