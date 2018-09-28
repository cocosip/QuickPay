using DotCommon.AutoMapper;
using DotCommon.Serializing;
using DotCommon.Threading;
using Microsoft.Extensions.DependencyInjection;
using QuickPay.Infrastructure.Extensions;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System;
using System.Threading.Tasks;
namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信H5支付
    /// </summary>
    public class WechatH5PayService : BaseWechatPayService, IWechatH5PayService
    {
        private readonly IJsonSerializer _jsonSerializer;
        public WechatH5PayService(IServiceProvider provider, IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider) : base(provider, wechatPayAppOverrideScopeProvider)
        {
            _jsonSerializer = provider.GetService<IJsonSerializer>();
        }

        /// <summary>H5支付统一下单,返回跳转的url地址
        /// </summary>
        public async Task<string> UnifiedOrder(H5UnifiedOrderInput input)
        {
            var request = input.MapTo<H5UnifiedOrderRequest>();
            var sceneInfoDict = SceneInfoCreator.CreateScene(input.SceneType, App);
            request.SceneInfo = sceneInfoDict.ToJson(_jsonSerializer);
            var response = await Executer.ExecuteAsync<H5UnifiedOrderResponse>(request, App);
            return response?.MWebUrl;
        }
    }
}
