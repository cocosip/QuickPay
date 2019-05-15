using DotCommon.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class MemoryPaymentStore : IPaymentStore
    {
        private const string PaymentListKey = "QuickPay.PaymentList";
        private readonly IDistributedCache<List<Payment>> _paymentTableCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryPaymentStore(IDistributedCache<List<Payment>> paymentTableCache)
        {
            _paymentTableCache = paymentTableCache;
        }

        private async Task<List<Payment>> GetList()
        {
            var paymentList = (await _paymentTableCache.GetAsync(PaymentListKey)) ?? new List<Payment>();
            return paymentList;
        }
        private async Task UpdateList(List<Payment> paymentList)
        {
            await _paymentTableCache.SetAsync(PaymentListKey, paymentList);
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            var paymentList = await GetList();
            paymentList.Add(payment);
            await UpdateList(paymentList);
        }


        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var paymentList = await GetList();
            var payment = paymentList.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return payment;
        }

        /// <summary>根据平台Id,AppId,支付宝/微信返回的交易号,获取数据
        /// </summary>
        public async Task<Payment> GetByTransactionId(int payPlatId,string appId,string transactionId)
        {
            var paymentList = await GetList();
            var payment = paymentList.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransactionId == transactionId);
            return payment;
        }


        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            var paymentList = await GetList();
            var payment = paymentList.FirstOrDefault(x => x.UniqueId == uniqueId);
            return payment;
        }


    }
}
