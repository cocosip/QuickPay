using Microsoft.Extensions.Options;

namespace QuickPay.Alipay.Apps
{
    /// <summary>文件形式存储支付宝配置信息
    /// </summary>
    public class FileAlipayConfigStore : IAlipayConfigStore
    {
        private readonly ConfigWrapper _configWrapper;

        /// <summary>Ctor
        /// </summary>
        public FileAlipayConfigStore(IOptions<ConfigWrapper> configWrapper)
        {
            _configWrapper = configWrapper.Value;
        }

        /// <summary>根据配置文件Id获取支付宝配置信息
        /// </summary>
        public AlipayConfig GetConfig(string id)
        {
            return _configWrapper.AlipayConfig;
        }

        /// <summary>根据应用AppId查询出配置文件
        /// </summary>
        public AlipayConfig GetConfigByAppId(string appId)
        {
            return _configWrapper.AlipayConfig;
        }


    }
}
