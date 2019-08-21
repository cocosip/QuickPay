using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Requests;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Services.DTOs;
using QuickPay.WeChatPay.Util;
using System;
using System.Threading.Tasks;
namespace QuickPay.WeChatPay.Services.Impl
{
    /// <summary>微信H5支付
    /// </summary>
    public class WeChatH5PayService : BaseWeChatPayService, IWeChatH5PayService
    {
        private readonly WeChatPayDataHelper _wechatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WeChatH5PayService(IServiceProvider provider, WeChatPayDataHelper wechatPayDataHelper) : base(provider)
        {
            _wechatPayDataHelper = wechatPayDataHelper;
        }

        /// <summary>H5支付统一下单,返回跳转的url地址
        /// </summary>
        public async Task<string> UnifiedOrder(H5UnifiedOrderInput input)
        {
            if (input.NotifyType != null && input.NotifyUrl.IsNullOrWhiteSpace())
            {
                input.NotifyUrl = NotifyTypeFinder.FindUrlFragments(input.NotifyType);
            }

            var request = ObjectMapper.Map<H5UnifiedOrderRequest>(input);
            var sceneInfoDict = SceneInfoCreator.CreateScene(input.SceneType, App);
            request.SceneInfo = _wechatPayDataHelper.DictToJson(sceneInfoDict);
            //sceneInfoDict.ToJson(_jsonSerializer);
            var response = await Executer.ExecuteAsync<H5UnifiedOrderResponse>(request, Config, App);
            return response?.MWebUrl;
        }
    }
}
