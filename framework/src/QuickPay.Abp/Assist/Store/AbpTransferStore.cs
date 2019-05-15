using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>转账存储
    /// </summary>
    public class AbpTransferStore : ITransferStore
    {
        private readonly IRepository<AbpTransfer> _abpTransferRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpTransferStore(IRepository<AbpTransfer> abpTransferRepository)
        {
            _abpTransferRepository = abpTransferRepository;
        }

        /// <summary>创建或者修改转账信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Transfer transfer)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.UniqueId == transfer.UniqueId && x.PayPlatId == transfer.PayPlatId);
            if (abpTransfer == null)
            {
                //Create
                abpTransfer = transfer.MapTo<AbpTransfer>();
                await _abpTransferRepository.InsertAsync(abpTransfer);
            }
            else
            {
                abpTransfer.MapTo(abpTransfer);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return abpTransfer.MapTo<Transfer>();
        }

        /// <summary>根据平台Id,AppId,微信支付宝返回的交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetByTransferNo(int payPlatId, string appId, string transferNo)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransferNo == transferNo);
            return abpTransfer.MapTo<Transfer>();
        }

        /// <summary>根据UniqueId获取转账信息
        /// </summary>
        public async Task<Transfer> GetByUniqueIdAsync(string uniqueId)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return abpTransfer.MapTo<Transfer>();
        }
    }
}
