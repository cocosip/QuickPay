using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Authentication
{
    /// <summary>
    /// </summary>
    public class EmptyAccessTokenStore : IAccessTokenStore
    {
        private readonly ILogger _logger;
        /// <summary>
        /// </summary>
        public EmptyAccessTokenStore(ILogger<EmptyAccessTokenStore> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// </summary>
        public Task CreateOrUpdateAccessTokenAsync(AccessToken info)
        {
            _logger.LogWarning($"未实现方法:EmptyAccessTokenStore.CreateOrUpdateAccessTokenAsync");
            return Task.FromResult(0);
        }

        /// <summary>
        /// </summary>
        public Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            _logger.LogWarning($"未实现方法:EmptyAccessTokenStore.GetAccessTokenAsync");
            return Task.FromResult<AccessToken>(null);
        }
    }
}
