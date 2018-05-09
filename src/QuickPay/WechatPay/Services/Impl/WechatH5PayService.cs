using DotCommon.AutoMapper;
using DotCommon.Runtime;
using QuickPay.Infrastructure.Extensions;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Requests;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Services.DTOs;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Services.Impl
{
    /// <summary>微信H5支付
    /// </summary>
    public class WechatH5PayService : BaseWechatPayService, IWechatH5PayService
    {
        public WechatH5PayService(IAmbientScopeProvider<WechatPayAppOverride> wechatPayAppOverrideScopeProvider) : base(wechatPayAppOverrideScopeProvider)
        {
        }

        /// <summary>H5支付统一下单,返回跳转的url地址
        /// </summary>
        public async Task<string> UnifiedOrder(H5UnifiedOrderInput input)
        {
            var request = input.MapTo<H5UnifiedOrderRequest>();
            var sceneInfoDict = SceneInfoCreator.CreateScene(input.SceneType, App);
            request.SceneInfo = sceneInfoDict.ToJson();
            var response = await Executer.ExecuteAsync<H5UnifiedOrderResponse>(request, App);
            return response?.MWebUrl;
        }
    }
}
