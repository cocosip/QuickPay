using QuickPay.Notify;
using System;
using System.Collections.Generic;

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

        /// <summary>是否开启微信沙盒
        /// </summary>
        public bool EnabledWechatPaySandbox { get; set; }

        /// <summary>是否开启支付宝沙盒
        /// </summary>
        public bool EnabledAlipaySandbox { get; set; }

        /// <summary>通知定义
        /// </summary>
        public List<NotifyDefination> NotifyDefinations { get; set; } = new List<NotifyDefination>();

    }
}
