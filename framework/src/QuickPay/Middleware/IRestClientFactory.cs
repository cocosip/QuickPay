using RestSharp;

namespace QuickPay.Middleware
{
    /// <summary>发起Http请求的客户端工厂
    /// </summary>
    public interface IRestClientFactory
    {

        /// <summary>根据Url地址获取RestClient
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IRestClient GetOrAddClient(string url);
    }
}
