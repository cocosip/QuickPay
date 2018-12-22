using DotCommon.AutoMapper;
using DotCommon.Extensions;
using DotCommon.Threading;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;
namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信H5支付
    /// </summary>
    public class WechatH5PayService : BaseWechatPayService, IWechatH5PayService
    {
        private readonly WechatPayDataHelper _wechatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WechatH5PayService(IServiceProvider provider, WechatPayDataHelper wechatPayDataHelper) : base(provider)
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

            var request = input.MapTo<H5UnifiedOrderRequest>();
            var sceneInfoDict = SceneInfoCreator.CreateScene(input.SceneType, App);
            request.SceneInfo = _wechatPayDataHelper.DictToJson(sceneInfoDict);
            //sceneInfoDict.ToJson(_jsonSerializer);
            var response = await Executer.ExecuteAsync<H5UnifiedOrderResponse>(request, App);
            return response?.MWebUrl;
        }
    }
}
