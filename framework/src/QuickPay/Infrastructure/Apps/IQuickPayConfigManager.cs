namespace QuickPay.Infrastructure.Apps
{
    /// <summary>QuickPay配置管理
    /// </summary>
    public interface IQuickPayConfigManager
    {

        /// <summary>根据Provider名称获取当前配置
        /// </summary>
        QuickPayConfig GetCurrentConfig(string providerName);
    }
}
