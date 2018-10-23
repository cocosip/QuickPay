﻿using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using QuickPay.PayAux;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class AbpJsApiTicketStore : IJsApiTicketStore
    {
        private readonly IRepository<AbpJsApiTicket> _abpJsApiTicketRepository;
        public AbpJsApiTicketStore(IRepository<AbpJsApiTicket> abpJsApiTicketRepository)
        {
            _abpJsApiTicketRepository = abpJsApiTicketRepository;
        }

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

        public async Task<JsApiTicket> GetJsApiTicketAsync(string appId)
        {
            var abpJsApiTicket = await _abpJsApiTicketRepository.FirstOrDefaultAsync(x => x.AppId == appId);
            return abpJsApiTicket.MapTo<JsApiTicket>();
        }
    }
}
