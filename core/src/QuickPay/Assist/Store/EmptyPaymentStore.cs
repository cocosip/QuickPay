using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>
    /// </summary>
    public class EmptyPaymentStore : IPaymentStore
    {
        private readonly ILogger _logger;
        /// <summary>
        /// </summary>
        public EmptyPaymentStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// </summary>
        public Task CreateOrUpdateAsync(Payment payment)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.CreateOrUpdateAsync");
            return Task.FromResult(0);
        }
        /// <summary>
        /// </summary>
        public Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.GetAsync");
            return Task.FromResult<Payment>(null);
        }
        /// <summary>
        /// </summary>
        public Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.GetByUniqueIdAsync");
            return Task.FromResult<Payment>(null);
        }

    }
}
