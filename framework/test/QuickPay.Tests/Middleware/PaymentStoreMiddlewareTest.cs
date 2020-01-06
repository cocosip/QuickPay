using DotCommon.Serializing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuickPay.Alipay.Utility;
using QuickPay.Assist.Store;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace QuickPay.Tests.Middleware
{
    public class PaymentStoreMiddlewareTest
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;
        private readonly Mock<IPaymentStore> _mockPaymentStore;
        private readonly Mock<IRequestTypeFinder> _mockRequestTypeFinder;
        private readonly Mock<IJsonSerializer> _mockJsonSerializer;

        private readonly AlipayPayDataHelper _alipayPayDataHelper;
        private readonly WeChatPayDataHelper _weChatPayDataHelper;

        public PaymentStoreMiddlewareTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            _provider = services.BuildServiceProvider();

            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();
            _mockPaymentStore = new Mock<IPaymentStore>();
            _mockRequestTypeFinder = new Mock<IRequestTypeFinder>();
            _mockJsonSerializer = new Mock<IJsonSerializer>();

            _alipayPayDataHelper = new AlipayPayDataHelper(_mockJsonSerializer.Object);
            _weChatPayDataHelper = new WeChatPayDataHelper(_mockJsonSerializer.Object);
        }

        [Fact]
        public void Invoke_Error_Test()
        {

            var paymentStoreMiddleware = new PaymentStoreMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockPaymentStore.Object, _mockRequestTypeFinder.Object, _alipayPayDataHelper, _weChatPayDataHelper, _mockJsonSerializer.Object);

            var context = new ExecuteContext()
            {
            };

            paymentStoreMiddleware.Invoke(context).Wait();
            Assert.True(context.Errors.Any());
            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);
        }

    }
}
