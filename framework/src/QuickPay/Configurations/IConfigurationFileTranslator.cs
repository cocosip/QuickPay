namespace QuickPay.Configurations
{
    /// <summary>配置文件转化
    /// </summary>
    public interface IConfigurationFileTranslator
    {
        /// <summary>读取支付宝和微信配置
        /// </summary>
        ConfigWrapper TranslateToConfigWapper(string file, string format = QuickPaySettings.ConfigFormat.Xml);

        /// <summary>将配置转换成文件内容
        /// </summary>
        string TranslateToText(ConfigWrapper configWapper, string format = QuickPaySettings.ConfigFormat.Xml);
    }
}
