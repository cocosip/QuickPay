namespace QuickPay.Configurations
{
    public interface IQuickPayConfigurationFileLoader
    {
        /// <summary>读取支付宝和微信配置
        /// </summary>
        ConfigWapper LoadConfigWapper(string file, string format = QuickPaySettings.ConfigFormat.Json);
    }
}
