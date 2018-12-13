using DotCommon.Caching;
using System;

namespace QuickPay.WechatPay.Authentication.Model
{
    /// <summary>微信认证状态缓存实体
    /// </summary>
    [CacheName(WechatPaySettings.AuthenticationState)]
    [Serializable]
    public class WechatPayAuthenticationStateCacheItem
    {
        /// <summary>Ctor
        /// </summary>
        public WechatPayAuthenticationStateCacheItem()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="state">状态</param>
        public WechatPayAuthenticationStateCacheItem(string state)
        {
            State = state;
        }

        /// <summary>状态
        /// </summary>
        public string State { get; set; }
    }
}
