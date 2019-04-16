using QuickPay.Alipay.Apps;
using QuickPay.WeChatPay.Apps;

namespace QuickPay
{
    /// <summary>配置文件包裹类
    /// </summary>
    public class ConfigWapper
    {
        /// <summary>支付宝配置
        /// </summary>
        public AlipayConfig AlipayConfig { get; set; }

        /// <summary>微信配置
        /// </summary>
        public WeChatPayConfig WechatPayConfig { get; set; }
    }
}
