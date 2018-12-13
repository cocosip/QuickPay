using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    /// <summary>支付信息存储
    /// </summary>
    public class AbpPaymentStore : IPaymentStore
    {
        private readonly IRepository<AbpPayment> _abpPaymentRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpPaymentStore(IRepository<AbpPayment> abpPaymentRepository)
        {
            _abpPaymentRepository = abpPaymentRepository;
        }

        /// <summary>创建或者修改支付信息
        /// </summary>
        public async Task CreateOrUpdateAsync(Payment payment)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.UniqueId == payment.UniqueId && x.PayPlatId == payment.PayPlatId);
            if (abpPayment == null)
            {
                //Create
                abpPayment = payment.MapTo<AbpPayment>();
                await _abpPaymentRepository.InsertAsync(abpPayment);
            }
            else
            {
                payment.MapTo(abpPayment);
            }
        }

        /// <summary>根据平台Id,AppId,交易号,获取数据
        /// </summary>
        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return abpPayment.MapTo<Payment>();
        }

        /// <summary>根据UniqueId获取支付信息
        /// </summary>
        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return abpPayment.MapTo<Payment>();
        }

    }
}
