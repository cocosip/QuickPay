using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QuickPay.PayAux.Store
{
    public class EmptyPaymentStore : IPaymentStore
    {
        private readonly ILogger _logger;
        public EmptyPaymentStore(ILogger<QuickPayLoggerName> logger)
        {
            _logger = logger;
        }
        public Task CreateOrUpdateAsync(Payment payment)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.CreateOrUpdateAsync");
            return Task.FromResult(0);
        }

        public Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.GetAsync");
            return Task.FromResult<Payment>(null);
        }

        public Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            _logger.LogWarning($"未实现方法:EmptyPaymentStore.GetByUniqueIdAsync");
            return Task.FromResult<Payment>(null);
        }

    }
}
