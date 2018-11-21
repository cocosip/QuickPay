namespace QuickPay.Configurations
{
    public interface IConfigurationFileTranslator
    {
        /// <summary>读取支付宝和微信配置
        /// </summary>
        ConfigWapper TranslateToConfigWapper(string file, string format = QuickPaySettings.ConfigFormat.Json);
    }
}
