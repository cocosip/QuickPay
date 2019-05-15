using DotCommon.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>转账存储
    /// </summary>
    public class MemoryTransferStore : ITransferStore
    {
        private const string TransferListKey = "QuickPay.TransferList";
        private readonly IDistributedCache<List<Transfer>> _transferTableCache;

        /// <summary>Ctor
        /// </summary>
        public MemoryTransferStore(IDistributedCache<List<Transfer>> transferTableCache)
        {
            _transferTableCache = transferTableCache;
        }

        private async Task<List<Transfer>> GetList()
        {
            var transferList = (await _transferTableCache.GetAsync(TransferListKey)) ?? new List<Transfer>();
            return transferList;
        }

        private async Task UpdateList(List<Transfer> transferList)
        {
            await _transferTableCache.SetAsync(TransferListKey, transferList);
        }

        /// <summary>创建或者修改转账信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Transfer transfer)
        {
            var transferList = await GetList();
            transferList.Add(transfer);
            await UpdateList(transferList);
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var transferList = await GetList();
            var transfer = transferList.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return transfer;
        }

        /// <summary>根据平台Id,AppId,微信支付宝返回的交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetByTransferNo(int payPlatId, string appId, string transferNo)
        {
            var transferList = await GetList();
            var transfer = transferList.FirstOrDefault(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransferNo == transferNo);
            return transfer;
        }

        /// <summary>根据UniqueId获取转账信息
        /// </summary>
        public async Task<Transfer> GetByUniqueIdAsync(string uniqueId)
        {
            var transferList = await GetList();
            var transfer = transferList.FirstOrDefault(x => x.UniqueId == uniqueId);
            return transfer;
        }

    }
}
