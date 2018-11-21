using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using QuickPay.Assist;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class AbpAccessTokenStore : IAccessTokenStore
    {
        private readonly IRepository<AbpAccessToken> _abpAccessTokenRepository;
        public AbpAccessTokenStore(IRepository<AbpAccessToken> abpAccessTokenRepository)
        {
            _abpAccessTokenRepository = abpAccessTokenRepository;
        }

        public async Task CreateOrUpdateAccessTokenAsync(AccessToken info)
        {
            var abpAccessToken = await _abpAccessTokenRepository.FirstOrDefaultAsync(x => x.AppId == info.AppId);
            if (abpAccessToken == null)
            {
                abpAccessToken = info.MapTo<AbpAccessToken>();
                await _abpAccessTokenRepository.InsertAsync(abpAccessToken);
            }
            else
            {
                abpAccessToken.Token = info.Token;
                abpAccessToken.ExpiredIn = info.ExpiredIn;
                abpAccessToken.LastModifiedTime = info.LastModifiedTime;
            }
        }

        public async Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            var abpAccessToken = await _abpAccessTokenRepository.FirstOrDefaultAsync(x => x.AppId == appId);
            return abpAccessToken.MapTo<AccessToken>();
        }
    }
}
