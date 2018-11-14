using System.ComponentModel;

namespace QuickPay.Configurations
{
    /// <summary>从哪里读取配置
    /// </summary>
    public enum ConfigSourceType
    {
        ///// <summary>从数据库获取配置
        ///// </summary>
        //[Description("从数据库获取配置")]
        //FromDb = 1,

        /// <summary>从配置文件中获取配置
        /// </summary>
        [Description("从配置文件中获取配置")]
        FromConfigFile = 2,

        /// <summary>从代码中获取配置
        /// </summary>
        [Description("从代码中获取配置")]
        FromClass = 4
    }
}
