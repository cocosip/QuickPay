using Abp.Domain.Repositories;
using DotCommon.ObjectMapping;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>转账存储
    /// </summary>
    public class AbpTransferStore : ITransferStore
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<AbpTransfer> _abpTransferRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpTransferStore(IObjectMapper objectMapper, IRepository<AbpTransfer> abpTransferRepository)
        {
            _objectMapper = objectMapper;
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
                abpTransfer = _objectMapper.Map<AbpTransfer>(transfer);
                await _abpTransferRepository.InsertAsync(abpTransfer);
            }
            else
            {
                _objectMapper.Map(transfer, abpTransfer);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return _objectMapper.Map<Transfer>(abpTransfer);
        }

        /// <summary>根据平台Id,AppId,微信支付宝返回的交易号,获取数据
        /// </summary>
        public async Task<Transfer> GetByTransferNo(int payPlatId, string appId, string transferNo)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.TransferNo == transferNo);
            return _objectMapper.Map<Transfer>(abpTransfer);
        }

        /// <summary>根据UniqueId获取转账信息
        /// </summary>
        public async Task<Transfer> GetByUniqueIdAsync(string uniqueId)
        {
            var abpTransfer = await _abpTransferRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return _objectMapper.Map<Transfer>(abpTransfer);
        }
    }
}
