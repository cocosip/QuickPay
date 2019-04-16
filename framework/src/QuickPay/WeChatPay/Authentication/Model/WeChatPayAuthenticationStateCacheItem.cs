using DotCommon.Caching;
using System;

namespace QuickPay.WeChatPay.Authentication.Model
{
    /// <summary>微信认证状态缓存实体
    /// </summary>
    [CacheName(WeChatPaySettings.AuthenticationState)]
    [Serializable]
    public class WeChatPayAuthenticationStateCacheItem
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatPayAuthenticationStateCacheItem()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="state">状态</param>
        public WeChatPayAuthenticationStateCacheItem(string state)
        {
            State = state;
        }

        /// <summary>状态
        /// </summary>
        public string State { get; set; }
    }
}
