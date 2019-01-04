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
        private const string PaymentTableKey = "QuickPay.PaymentTable";
        private readonly IDistributedCache<List<Payment>> _paymentTableCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryPaymentStore(IDistributedCache<List<Payment>> paymentTableCache)
        {
            _paymentTableCache = paymentTableCache;
        }

        private async Task<List<Payment>> GetTable()
        {
            var table = await _paymentTableCache.GetAsync(PaymentTableKey);
            if (table == null)
            {
                table = new List<Payment>();
            }
            return table;
        }
        private async Task UpdateTable(List<Payment> paymentTable)
        {
            await _paymentTableCache.SetAsync(PaymentTableKey, paymentTable);
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            var paymentTable = await GetTable();
            paymentTable.Add(payment);
            await UpdateTable(paymentTable);
        }


        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var paymentTable = await GetTable();
            var payment = paymentTable.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return payment;
        }


        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            var paymentTable = await GetTable();
            var payment = paymentTable.FirstOrDefault(x => x.UniqueId == uniqueId);
            return payment;
        }


    }
}
