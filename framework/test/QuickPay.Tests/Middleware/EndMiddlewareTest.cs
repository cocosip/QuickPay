using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuickPay.Middleware;
using System;
using Xunit;

namespace QuickPay.Tests.Middleware
{
    public class EndMiddlewareTest
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;

        public EndMiddlewareTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            _provider = services.BuildServiceProvider();

            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();

        }

        [Fact]
        public void Invoke_Test()
        {
            var endMiddleware = new EndMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);
            endMiddleware.Invoke(new ExecuteContext()).Wait();
            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);
        }
    }
}
