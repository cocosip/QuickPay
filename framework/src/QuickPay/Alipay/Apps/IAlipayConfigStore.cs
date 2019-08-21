namespace QuickPay.Alipay.Apps
{
    /// <summary>存储配置信息
    /// </summary>
    public interface IAlipayConfigStore
    {
        /// <summary>根据配置文件Id获取支付宝配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlipayConfig GetConfig(string id);

        /// <summary>根据应用AppId查询出配置文件
        /// </summary>
        AlipayConfig GetConfigByAppId(string appId);
    }
}
