using DotCommon.Caching;
using System;

namespace QuickPay.WechatPay.Authentication.Model
{
    [CacheName(WechatPaySettings.AuthenticationState)]
    [Serializable]
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
