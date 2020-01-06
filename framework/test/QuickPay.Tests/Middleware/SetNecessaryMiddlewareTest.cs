using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuickPay.Configurations;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Requests;
using System;
using System.Linq;
using Xunit;

namespace QuickPay.Tests.Middleware
{
    public class SetNecessaryMiddlewareTest
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;

        public SetNecessaryMiddlewareTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            _provider = services.BuildServiceProvider();

            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();
        }

        [Fact]
        public void Invoke_Error_Test()
        {
            var setNecessaryMiddleware = new SetNecessaryMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);
            var context = new ExecuteContext();
            setNecessaryMiddleware.Invoke(context).Wait();
            Assert.True(context.Errors.Any());
            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);
        }

        [Fact]
        public void Invoke_Test()
        {
            var setNecessaryMiddleware = new SetNecessaryMiddleware(_provider, _mockQuickPayExecuteDelegate.Object);
            var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper("QuickPayConfig.xml");

            var request = new AppUnifiedOrderRequest("body1", "00001", 100);
            var app = wrapper.WeChatPayConfig.GetDefaultApp();
            var context = new ExecuteContext()
            {
                Config = wrapper.WeChatPayConfig,
                App = app,
                SignFieldName = "",
                Request = request
            };
            Assert.Empty(context.Request.SignTypeName);
            Assert.Null(request.AppId);
            Assert.Null(request.MchId);
            setNecessaryMiddleware.Invoke(context).Wait();
            
            Assert.NotEmpty(context.Request.SignTypeName);
            Assert.Equal(app.AppId, ((AppUnifiedOrderRequest)context.Request).AppId);
            Assert.Equal(app.MchId, ((AppUnifiedOrderRequest)context.Request).MchId);

            Assert.Equal(context.Request.SignTypeName, context.SignType);
            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);
        }

    }
}
