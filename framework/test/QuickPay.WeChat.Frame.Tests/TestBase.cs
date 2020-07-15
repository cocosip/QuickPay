using DotCommon.Json4Net;
using DotCommon.Serializing;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Tests
{
    public class TestBase
    {
        protected IJsonSerializer DefaultNewtonsoftJsonSerializer { get; set; }

        public TestBase()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>() { new IsoDateTimeConverter() },
                ContractResolver = new CustomContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
            IOptions<JsonSerializerSettings> settings = Options.Create<JsonSerializerSettings>(jsonSerializerSettings);
            DefaultNewtonsoftJsonSerializer = new NewtonsoftJsonSerializer(settings);
        }

        protected HttpClient BuildMockHttpClient(HttpResponseMessage httpResponseMessage)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>()
            {
                CallBase = true
            };
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);
            var httpClient = new HttpClient(mockMessageHandler.Object);
            return httpClient;
        }


    }
}
