using DotCommon.Caching;

namespace QuickPay.WechatPay.Authentication.Model
{
    [CacheName(WechatPaySettings.AuthenticationState)]
    public class WechatPayAuthenticationStateCacheItem
    {
        public WechatPayAuthenticationStateCacheItem()
        {

        }
        public WechatPayAuthenticationStateCacheItem(string state)
        {
            State = state;
        }

        public string State { get; set; }
    }
}
