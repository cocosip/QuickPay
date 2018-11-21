using System.ComponentModel;

namespace QuickPay.Assist
{
    /// <summary>支付平台,支付宝和微信
    /// </summary>
    public enum PayPlat
    {
        [Description("未知")]
        Unknow = -1,

        [Description("支付宝")]
        Alipay = 1,

        [Description("微信")]
        WechatPay = 2
    }
}
