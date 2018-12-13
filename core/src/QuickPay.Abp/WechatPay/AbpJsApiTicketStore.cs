using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using QuickPay.Assist;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>JsApiTicket存储
    /// </summary>
    public class AbpJsApiTicketStore : IJsApiTicketStore
    {
        private readonly IRepository<AbpJsApiTicket> _abpJsApiTicketRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpJsApiTicketStore(IRepository<AbpJsApiTicket> abpJsApiTicketRepository)
        {
            _abpJsApiTicketRepository = abpJsApiTicketRepository;
        }

        /// <summary>根据应用Id获取JsApiTicket
        /// </summary>
        public async Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            var abpJsApiTicket = await _abpJsApiTicketRepository.FirstOrDefaultAsync(x => x.AppId == appId);
            return abpJsApiTicket.MapTo<JsApiTicket>();
        }

        /// <summary>创建或者修改JsApiTicket
        /// </summary>
        public async Task CreateOrUpdateJsApiTicketAsync(JsApiTicket jsApiTicket)
        {
            var abpJsApiTicket = await _abpJsApiTicketRepository.FirstOrDefaultAsync(x => x.AppId == jsApiTicket.AppId);
            if (abpJsApiTicket == null)
            {
                abpJsApiTicket = jsApiTicket.MapTo<AbpJsApiTicket>();
                await _abpJsApiTicketRepository.InsertAsync(abpJsApiTicket);
            }
            else
            {
                abpJsApiTicket.Ticket = jsApiTicket.Ticket;
                abpJsApiTicket.ExpiredIn = jsApiTicket.ExpiredIn;
                abpJsApiTicket.LastModifiedTime = jsApiTicket.LastModifiedTime;
            }
        }

    }
}
