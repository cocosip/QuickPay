using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class EmptyAccessTokenStore : IAccessTokenStore
    {
        private readonly ILogger _logger;
        public EmptyAccessTokenStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger = logger;
        }



        public Task CreateOrUpdateAccessTokenAsync(AccessToken info)
        {
            _logger.LogWarning($"未实现方法:EmptyAccessTokenStore.CreateOrUpdateAccessTokenAsync");
            return Task.FromResult(0);
        }

        public Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            _logger.LogWarning($"未实现方法:EmptyAccessTokenStore.GetAccessTokenAsync");
            return Task.FromResult<AccessToken>(null);
        }
    }
}
