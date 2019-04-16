using System.ComponentModel;

namespace QuickPay.Configurations
{
    /// <summary>从哪里读取配置
    /// </summary>
    public enum ConfigSourceType
    {
        /// <summary>从配置文件中获取配置
        /// </summary>
        [Description("从配置文件中获取配置")]
        FromConfigFile = 1,

        /// <summary>从代码中获取配置
        /// </summary>
        [Description("从代码中获取配置")]
        FromClass = 2
    }
}
