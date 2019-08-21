namespace QuickPay.WeChatPay.Apps
{
    /// <summary>微信支付Override信息
    /// </summary>
    public class WeChatPayOverride
    {
        /// <summary>配置文件
        /// </summary>
        public WeChatPayConfig Config { get; set; }

        /// <summary>应用信息
        /// </summary>
        public WeChatPayApp App { get; set; }

        /// <summary>Ctor
        /// </summary>
        public WeChatPayOverride()
        {
        }

        /// <summary>Ctor
        /// </summary>
        public WeChatPayOverride(WeChatPayConfig config, WeChatPayApp app)
        {
            Config = config;
            App = app;
        }
    }
}
