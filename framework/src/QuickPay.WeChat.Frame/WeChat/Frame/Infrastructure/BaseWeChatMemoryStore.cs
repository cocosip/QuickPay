namespace QuickPay.WeChat.Frame.Infrastructure
{
    /// <summary>微信缓存基类
    /// </summary>
    public abstract class BaseWeChatMemoryStore
    {
        public BaseWeChatMemoryStore()
        {
        }

        /// <summary>格式化缓存Key
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="appId">应用AppId</param>
        /// <param name="typeName">类型</param>
        /// <returns></returns>
        protected string FormatCacheKey(string tenantId, string appId)
        {
            return $"QuickPay.WeChat.Frame:{tenantId}#{appId}";
        }

    }
}
