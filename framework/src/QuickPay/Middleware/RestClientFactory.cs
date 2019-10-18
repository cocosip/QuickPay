using RestSharp;
using System.Collections.Concurrent;

namespace QuickPay.Middleware
{
    /// <summary>发起Http请求的客户端工厂
    /// </summary>
    public class RestClientFactory : IRestClientFactory
    {
        private readonly ConcurrentDictionary<string, IRestClient> restClientDict = new ConcurrentDictionary<string, IRestClient>();

        /// <summary>根据Url地址获取RestClient
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IRestClient GetOrAddClient(string url)
        {
            var key = url.ToLower().TrimEnd('/');

            if (restClientDict.TryGetValue(key, out IRestClient client))
            {
                return client;
            }
            else
            {
                client = new RestClient(url);
                restClientDict.TryAdd(key, client);
                return client;
            }
        }

    }
}
