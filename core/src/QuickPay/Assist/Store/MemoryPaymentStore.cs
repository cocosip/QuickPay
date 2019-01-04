using DotCommon.Caching;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class MemoryPaymentStore : IPaymentStore
    {
        private readonly IDistributedCache<Payment> _paymentCache;
        private readonly IDistributedCache<PaymentMapperItem> _paymentMapperItemCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryPaymentStore(IDistributedCache<Payment> paymentCache, IDistributedCache<PaymentMapperItem> paymentMapperItemCache)
        {
            _paymentCache = paymentCache;
            _paymentMapperItemCache = paymentMapperItemCache;
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            await _paymentCache.SetAsync(FormatPaymentKey(payment.UniqueId), payment);
            //item
            var paymentMapperItem = new PaymentMapperItem(payment.UniqueId, payment.PayPlatId, payment.AppId, payment.OutTradeNo);
            await _paymentMapperItemCache.SetAsync(paymentMapperItem.GetFormatKey(), paymentMapperItem);
        }


        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            //如何做到UniqueId与三者之间的相同?
            var paymentMapperItem = new PaymentMapperItem("", payPlatId, appId, outTradeNo);
            var queryPaymentMapperItem = await _paymentMapperItemCache.GetAsync(paymentMapperItem.GetFormatKey());
            if (queryPaymentMapperItem != null)
            {
                return await GetByUniqueIdAsync(queryPaymentMapperItem.UniqueId);
            }
            return default(Payment);
        }


        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            return await _paymentCache.GetAsync(FormatPaymentKey(uniqueId));
        }

        /// <summary>支付存储的Key
        /// </summary>
        private string FormatPaymentKey(string uniqueId)
        {
            return $"QuickPay:Payment:{uniqueId}";
        }
    }
}
