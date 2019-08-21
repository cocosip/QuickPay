namespace QuickPay.WeChatPay.Apps
{
    /// <summary>存储配置信息
    /// </summary>
    public interface IWeChatPayConfigStore
    {
        /// <summary>根据配置文件Id获取微信配置信息
        /// </summary>
        WeChatPayConfig GetConfig(string id);

        /// <summary>根据应用AppId查询出配置文件
        /// </summary>
        WeChatPayConfig GetByAppId(string appId);
    }
}
