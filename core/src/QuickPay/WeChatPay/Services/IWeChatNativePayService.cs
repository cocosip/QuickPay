using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services
{
    /// <summary>微信扫码支付
    /// </summary>
    public interface IWeChatNativePayService : IWeChatPayService
    {
        /// <summary>当使用扫码支付模式2时,统一下单,返回二维码地址
        /// </summary>
        Task<string> Mode2Unifiedorder(NativeMode2UnifiedOrderInput input);


        /// <summary>使用扫码支付模式1,生成被客户端扫描的二维码
        /// </summary>
        Task<string> Mode1CreateCode(NativeMode1CreateCodeInput input);

        /// <summary>当使用扫码支付模式1时,统一下单,返回预订单PrepayId给【微信】
        /// 注:NativeMode1UnifiedOrderInput 是扫码支付模式1下异步通知,在对通知进行验证,提取之后的包装
        /// </summary>
        Task<NativeMode1UnifiedOrderOutputResponse> Mode1UnifiedOrder(NativeMode1UnifiedOrderInput input);
    }
}
