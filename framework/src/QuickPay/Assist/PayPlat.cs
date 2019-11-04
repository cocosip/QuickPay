using System.ComponentModel;

namespace QuickPay.Assist
{
    /// <summary>支付平台,支付宝和微信
    /// </summary>
    public enum PayPlat
    {
        /// <summary>未知
        /// </summary>
        [Description("未知")]
        Unknow = -1,

        /// <summary>支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 1,

        /// <summary>微信
        /// </summary>
        [Description("微信")]
        WeChatPay = 2
    }
}
