using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.WeChat.Frame.Infrastructure;
using QuickPay.WeChat.Frame.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.WeChat.Frame.Service
{
    /// <summary>微信Sdk-Ticket服务
    /// </summary>
    public class WeChatSdkTicketService : WeChatFrameServiceBase, IWeChatSdkTicketService
    {
        private readonly IWeChatSdkTicketStore _weChatSdkTicketStore;
        private readonly IWeChatAccessTokenService _weChatAccessTokenService;

        /// <summary>Ctor
        /// </summary>
        public WeChatSdkTicketService(IServiceProvider provider, ILogger<WeChatFrameServiceBase> logger, IWeChatSdkTicketStore weChatSdkTicketStore, IWeChatAccessTokenService weChatAccessTokenService) : base(provider, logger)
        {
            _weChatSdkTicketStore = weChatSdkTicketStore;
            _weChatAccessTokenService = weChatAccessTokenService;
        }

        /// <summary>获取可用的Sdk-Ticket(先从本地存储中获取,如果不存在,就从微信接口获取)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密钥,即appsecret</param>
        /// <param name="type"></param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<string> GetSdkTicketAsync(string appId, string appSecret, string type, string tenantId = "")
        {
            //从存储中读取SdkTicketModel
            var ticketType = type.ToUpper();
            var sdkTicketInfo = await _weChatSdkTicketStore.GetSdkTicketAsync(appId, ticketType, tenantId);
            if (sdkTicketInfo != null && !sdkTicketInfo.IsExpired(DateTime.Now))
            {
                return sdkTicketInfo.Ticket;
            }
            //从微信获取SdkTicket
            var sdkTicket = await GetRemoteSdkTicketAsync(appId, appSecret, type);
            sdkTicketInfo = new SdkTicketInfo()
            {
                TenantId = tenantId,
                AppId = appId,
                Ticket = sdkTicket.Ticket,
                ExpiredIn = sdkTicket.ExpiresIn,
                UpdateTime = DateTime.Now,
                TicketType = ticketType
            };
            //更新存储中的Sdk-Ticket
            await _weChatSdkTicketStore.CreateOrUpdateSdkTicketAsync(sdkTicketInfo);
            return sdkTicket.Ticket;

        }

        /// <summary>从微信接口获取微信Sdk-Ticket
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密钥,即appsecret</param>
        /// <param name="type"></param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<SdkTicket> GetRemoteSdkTicketAsync(string appId, string appSecret, string type, string tenantId = "")
        {
            //先拿到应用的AccessToken
            var accessToken = await _weChatAccessTokenService.GetAccessTokenAsync(appId, appSecret, tenantId);
            if (accessToken.IsNullOrWhiteSpace())
            {
                Logger.LogError("微信获取SdkTicket时,查询到的AccessToken为空");
                throw new ArgumentNullException("无法查询到有效的AccessToken");
            }
            var ticketType = type.ToUpper();

            var url = $"{WeChatFrameSettings.WeChatUrls.BaseUrl}{WeChatFrameSettings.WeChatUrls.SdkTicketResource}?access_token={accessToken}&type={type}";

            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"获取微信SdkTicket失败,请求url地址:[{url}],返回Http状态:{response.StatusCode}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            Logger.LogDebug(ParseLog(appId, "GetRemoteSdkTicketAsync", $"获取应用SdkTicket,类型:{type},返回结果:{responseString}"));

            var sdkTicket = JsonSerializer.Deserialize<SdkTicket>(responseString);
            //设置SdkTicket类型
            sdkTicket.Type = ticketType;
            return sdkTicket;

        }
    }
}
