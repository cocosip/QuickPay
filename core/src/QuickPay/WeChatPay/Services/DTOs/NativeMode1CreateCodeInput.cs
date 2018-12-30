using DotCommon.AutoMapper;
using QuickPay.WeChatPay.Requests;

namespace QuickPay.WeChatPay.Services.DTOs
{
    /// <summary>微信扫码支付(Native)模式1,生成二维码
    /// </summary>
    [AutoMapTo(typeof(NativeMode1CreateCodeRequest))]
    public class NativeMode1CreateCodeInput
    {
        /// <summary>产品Id
        /// </summary>
        public string ProductId { get; set; }
    }
}
