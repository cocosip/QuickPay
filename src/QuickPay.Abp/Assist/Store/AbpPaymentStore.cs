using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using System.Threading.Tasks;

namespace QuickPay.Assist.Store
{
    public class AbpPaymentStore : IPaymentStore
    {
        private readonly IRepository<AbpPayment> _abpPaymentRepository;
        public AbpPaymentStore(IRepository<AbpPayment> abpPaymentRepository)
        {
            _abpPaymentRepository = abpPaymentRepository;
        }

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

        public async Task<Payment> GetAsync(int payPlatId, string appId, string outTradeNo)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.PayPlatId == payPlatId && x.AppId == appId && x.OutTradeNo == outTradeNo);
            return abpPayment.MapTo<Payment>();
        }

        public async Task<Payment> GetByUniqueIdAsync(string uniqueId)
        {
            var abpPayment = await _abpPaymentRepository.FirstOrDefaultAsync(x => x.UniqueId == uniqueId);
            return abpPayment.MapTo<Payment>();
        }

    }
}
