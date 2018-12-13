using Abp.Domain.Repositories;
using DotCommon.AutoMapper;
using QuickPay.Assist;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>AccessToken存储
    /// </summary>
    public class AbpAccessTokenStore : IAccessTokenStore
    {
        private readonly IRepository<AbpAccessToken> _abpAccessTokenRepository;
        /// <summary>Ctor
        /// </summary>
        public AbpAccessTokenStore(IRepository<AbpAccessToken> abpAccessTokenRepository)
        {
            _abpAccessTokenRepository = abpAccessTokenRepository;
        }

        /// <summary>根据应用Id获取当前token
        /// </summary>
        public async Task<AccessToken> GetAccessTokenAsync(string appId)
        {
            var abpAccessToken = await _abpAccessTokenRepository.FirstOrDefaultAsync(x => x.AppId == appId);
            return abpAccessToken.MapTo<AccessToken>();
        }

      /// <summary>创建或者修改AccessToken
        /// </summary>
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

    }
}
