using DotCommon.Serializing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Utility;
using QuickPay.Assist;
using QuickPay.Assist.Store;
using QuickPay.Configurations;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void Invoke_Alipay_Test()
        {
            var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper("QuickPayConfig.xml");
            _mockRequestTypeFinder.Setup(x => x.FindPaymentStoreTypes()).Returns(new List<Type>()
            {
                typeof(AppTradePayRequest)
            });


            var paymentStoreMiddleware = new PaymentStoreMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockPaymentStore.Object, _mockRequestTypeFinder.Object, _alipayPayDataHelper, _weChatPayDataHelper, _mockJsonSerializer.Object);

            var bizContent = new AppTradeBizContentPayRequest("subject1", "body", "alipay0001", "1.00");

            var app = wrapper.AlipayConfig.GetDefaultApp();
            var context = new ExecuteContext()
            {
                Request = new AppTradePayRequest(bizContent, "http://127.0.0.1/alipay/notify"),
                RequestPayData = new PayData(),
                Config = wrapper.AlipayConfig,
                App = app
            };

            context.RequestPayData.SetValue("appid", app.AppId);

            Assert.Equal(QuickPaySettings.Provider.Alipay, context.Request.Provider);

            paymentStoreMiddleware.Invoke(context).Wait();
            _mockJsonSerializer.Verify(x => x.Serialize(It.IsAny<SortedDictionary<string, object>>()), Times.AtLeastOnce);

            _mockPaymentStore.Verify(x => x.CreateOrUpdateAsync(It.IsAny<Payment>()), Times.AtLeastOnce);

            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);

        }

        [Fact]
        public void Invoke_WeChatPay_Test()
        {
            var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper("QuickPayConfig.xml");
            _mockRequestTypeFinder.Setup(x => x.FindPaymentStoreTypes()).Returns(new List<Type>()
            {
                typeof(AppUnifiedOrderRequest)
            });


            var paymentStoreMiddleware = new PaymentStoreMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockPaymentStore.Object, _mockRequestTypeFinder.Object, _alipayPayDataHelper, _weChatPayDataHelper, _mockJsonSerializer.Object);



            var app = wrapper.WeChatPayConfig.GetDefaultApp();
            var context = new ExecuteContext()
            {
                Request = new AppUnifiedOrderRequest("body1", "WeChatPay0001", 100),
                RequestPayData = new PayData(),
                Config = wrapper.AlipayConfig,
                App = app
            };

            context.RequestPayData.SetValue("appid", app.AppId);
            context.RequestPayData.SetValue("total_fee", 100);
            context.RequestPayData.SetValue("out_trade_no", "WeChatPay0001");

            Assert.Equal(QuickPaySettings.Provider.WeChatPay, context.Request.Provider);

            paymentStoreMiddleware.Invoke(context).Wait();
            _mockJsonSerializer.Verify(x => x.Serialize(It.IsAny<SortedDictionary<string, object>>()), Times.AtLeastOnce);

            _mockPaymentStore.Verify(x => x.CreateOrUpdateAsync(It.IsAny<Payment>()), Times.AtLeastOnce);

            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);
        }
    }
}
