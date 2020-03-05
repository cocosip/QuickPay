//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using QuickPay.Configurations;
//using QuickPay.Middleware;
//using QuickPay.WeChatPay.Requests;
//using RestSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace QuickPay.Tests.Middleware
//{
//    public class ExecuterExecuteMiddlewareTest
//    {
//        private readonly IServiceProvider _provider;
//        private readonly Mock<QuickPayExecuteDelegate> _mockQuickPayExecuteDelegate;
//        private Mock<QuickPayConfigurationOption> _mockQuickPayConfigurationOption;
//        private readonly Mock<IRestClientFactory> _mockRestClientFactory;
//        private readonly Mock<IRestClient> _mockRestClient;

//        public ExecuterExecuteMiddlewareTest()
//        {
//            var services = new ServiceCollection();
//            services.AddLogging();
//            _provider = services.BuildServiceProvider();
//            _mockQuickPayExecuteDelegate = new Mock<QuickPayExecuteDelegate>();
//            _mockQuickPayConfigurationOption = new Mock<QuickPayConfigurationOption>();
//            _mockRestClientFactory = new Mock<IRestClientFactory>();
//            _mockRestClient = new Mock<IRestClient>();
//        }

//        [Fact]
//        public void Invoke_Error_Test()
//        {

//            var executerExecuteMiddleware = new ExecuterExecuteMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockQuickPayConfigurationOption.Object, _mockRestClientFactory.Object);
//            var context = new ExecuteContext()
//            {
//                Request = new AppUnifiedOrderRequest()
//                {

//                },
//                RequestHandler = QuickPaySettings.RequestHandler.Execute
//            };

//            executerExecuteMiddleware.Invoke(context).Wait();
//            Assert.True(context.Errors.Any());
//            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.Never);
//        }

//        [Fact]
//        public void Invoke_RequestHandler_Sign_Test()
//        {

//            var executerExecuteMiddleware = new ExecuterExecuteMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockQuickPayConfigurationOption.Object, _mockRestClientFactory.Object);
//            var context = new ExecuteContext()
//            {
//                RequestHandler = QuickPaySettings.RequestHandler.Sign
//            };

//            executerExecuteMiddleware.Invoke(context).Wait();
//            Assert.False(context.Errors.Any());
//            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);
//        }

//        [Fact]
//        public void Invoke_Test()
//        {
//            var wrapper = ConfigurationFileHelper.TranslateToConfigWrapper("QuickPayConfig.xml");


//            IRestResponse restResponse = new RestResponse()
//            {
//                Content = "Test1"
//            };
//            _mockRestClient.Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>())).Returns(Task.FromResult(restResponse));
//            _mockRestClientFactory.Setup(x => x.GetOrAddClient(It.IsAny<string>())).Returns(_mockRestClient.Object);


//            var executerExecuteMiddleware = new ExecuterExecuteMiddleware(_provider, _mockQuickPayExecuteDelegate.Object, _mockQuickPayConfigurationOption.Object, _mockRestClientFactory.Object);
//            var context = new ExecuteContext()
//            {
//                RequestHandler = QuickPaySettings.RequestHandler.Execute,
//                Config = wrapper.WeChatPayConfig,
//                Request = new AppUnifiedOrderRequest(),
//                HttpBuilder = new DotCommon.Http.HttpBuilder()
//                {
//                    Method = DotCommon.Http.Method.GET,
//                    Resource = ""
//                }
//            };

//            executerExecuteMiddleware.Invoke(context).Wait();
//            _mockQuickPayExecuteDelegate.Verify(x => x.Invoke(It.IsAny<ExecuteContext>()), Times.AtLeastOnce);
//        }
//    }
//}
