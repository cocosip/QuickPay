using Microsoft.Extensions.Options;

namespace QuickPay.WeChatPay.Apps
{
    /// <summary>文件形式存储微信配置信息
    /// </summary>
    public class FileWeChatPayConfigStore : IWeChatPayConfigStore
    {
        private readonly ConfigWrapper _configWrapper;

        /// <summary>Ctor
        /// </summary>
        public FileWeChatPayConfigStore(IOptions<ConfigWrapper> configWrapper)
        {
            _configWrapper = configWrapper.Value;
        }

        /// <summary>根据配置文件Id获取微信配置信息
        /// </summary>
        public WeChatPayConfig GetConfig(string id)
        {
            return _configWrapper.WeChatPayConfig;
        }

        /// <summary>根据应用AppId查询出配置文件
        /// </summary>
        public WeChatPayConfig GetByAppId(string appId)
        {
            return _configWrapper.WeChatPayConfig;
        }
    }
}
