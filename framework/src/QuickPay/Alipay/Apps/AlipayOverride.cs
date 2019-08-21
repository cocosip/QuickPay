namespace QuickPay.Alipay.Apps
{
    /// <summary>支付宝Override信息
    /// </summary>
    public class AlipayOverride
    {
        /// <summary>配置文件
        /// </summary>
        public AlipayConfig Config { get; set; }

        /// <summary>应用信息
        /// </summary>
        public AlipayApp App { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AlipayOverride()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public AlipayOverride(AlipayConfig config, AlipayApp app)
        {
            Config = config;
            App = app;
        }
    }
}
