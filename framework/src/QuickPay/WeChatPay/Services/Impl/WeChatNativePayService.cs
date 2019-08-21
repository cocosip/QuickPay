using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>扫码支付
    /// </summary>
    public class WeChatNativePayService : BaseWeChatPayService, IWeChatNativePayService
    {
        /// <summary>Ctor
        /// </summary>
        public WeChatNativePayService(IServiceProvider provider) : base(provider)
        {

        }

        /// <summary>当使用扫码支付模式2时,统一下单,返回二维码地址
        /// </summary>
        public async Task<string> Mode2Unifiedorder(NativeMode2UnifiedOrderInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var request = ObjectMapper.Map<NativeMode2UnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<NativeMode2UnifiedOrderResponse>(request,Config, App);
            return response?.CodeUrl;
        }

        /// <summary>使用扫码支付模式1,生成被客户端扫描的二维码
        /// </summary>
        public async Task<string> Mode1CreateCode(NativeMode1CreateCodeInput input)
        {
            var request = ObjectMapper.Map<NativeMode1CreateCodeRequest>(input);
            var response = await Executer.SignRequest<NativeMode1CreateCodeResponse>(request,Config, App);
            //将签名后的Response转换成二维码
            return response.ToCodeUrl();
        }

        /// <summary>当使用扫码支付模式1时,统一下单,返回预订单PrepayId给【微信】
        /// 注:NativeMode1UnifiedOrderInput 是扫码支付模式1下异步通知,在对通知进行验证,提取之后的包装
        /// </summary>
        public async Task<NativeMode1UnifiedOrderOutputResponse> Mode1UnifiedOrder(NativeMode1UnifiedOrderInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var request = ObjectMapper.Map<NativeMode1UnifiedOrderRequest>(input);
            var response = await Executer.ExecuteAsync<NativeMode1UnifiedOrderResponse>(request,Config, App);
            //响应与执行都成功
            if (response.ReturnSuccess && response.ResultSuccess)
            {
                var prepayId = response.PrepayId;
                if (!prepayId.IsNullOrWhiteSpace())
                {
                    //输出给微信,详情见https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=6_4  (输出参数)
                    var outputRequest = new NativeMode1UnifiedOrderOutputRequest(prepayId);
                    var outputResponse = await Executer.SignRequest<NativeMode1UnifiedOrderOutputResponse>(outputRequest, Config, App);
                    return outputResponse;
                }
            }
            Logger.LogError($"微信扫码支付,提交预订单出错,ReturnMsg:{response.ReturnMsg},ErrorCodeMsg:{response.ErrCodeDes}");
            throw new Exception(response.ReturnMsg);
        }

    }
}
