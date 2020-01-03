using Microsoft.Extensions.Logging;
using Moq;
using QuickPay.Alipay.Apps;
using QuickPay.Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace QuickPay.Tests.Middleware
{
    public class AutoUniqueIdMiddlewareTest
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;
        private readonly Mock<IServiceProvider> _mockIServiceProvider;
        public AutoUniqueIdMiddlewareTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            _provider = services.BuildServiceProvider();

            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();
            _mockIServiceProvider = new Mock<IServiceProvider>();

        }


        [Fact]
        public void Invoke_Test()
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

            context = new ExecuteContext()
            {
                Request = new QuickPay.Alipay.Requests.AppTradePayRequest()
                {
                    BusinessCode = "BusinessCode1"
                }
            };
            Assert.Null(context.Request.UniqueId);
            Assert.Equal("BusinessCode1", context.Request.BusinessCode);
            autoUniqueIdMiddleware.Invoke(context).Wait();
            Assert.NotEmpty(context.Request.UniqueId);


            //_mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);

        }

    }
}
