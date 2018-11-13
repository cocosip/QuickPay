namespace QuickPay.Configurations
{
    /// <summary>支付配置信息
    /// </summary>
    public class QuickPayConfigurationOption
    {
        /// <summary>配置来源类型
        /// </summary>
        public ConfigSourceType ConfigSourceType { get; set; } = ConfigSourceType.FromConfigFile;

        /// <summary>如果配置来源于配置文件,则字段不能省略,配置的文件名
        /// </summary>
        public string ConfigFileName { get; set; }

        /// <summary>配置的格式(默认XML)
        /// </summary>
        public string ConfigFileFormat { get; set; } = QuickPaySettings.ConfigFormat.Xml;

        /// <summary>数据库连接字符串
        /// </summary>
        public string DbConnectionStrings { get; set; }

        /// <summary>是否使用数据库连接去连接数据库
        /// </summary>
        public bool UseConnection { get; set; }

        /// <summary>从数据库中重新读取的时间间隔(以ms为单位)
        /// </summary>
        public int ReloadInterval { get; set; } = 60 * 1000;
    }
}
