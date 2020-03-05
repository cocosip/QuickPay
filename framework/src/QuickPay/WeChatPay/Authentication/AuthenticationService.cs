using DotCommon.Caching;
using DotCommon.Extensions;
using DotCommon.Serializing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using QuickPay.WeChatPay.Authentication.Model;
using QuickPay.WeChatPay.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Authentication
{
    /// <summary>认证服务
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenStore _accessTokenStore;
        private readonly IJsApiTicketStore _jsApiTicketStore;
        private readonly IDistributedCache<WeChatPayAuthenticationStateCacheItem> _stateCache;

        /// <summary>Ctor
        /// </summary>
        public AuthenticationService(ILogger<AuthenticationService> logger, IJsonSerializer jsonSerializer, IHttpClientFactory httpClientFactory, IAccessTokenStore accessTokenStore, IJsApiTicketStore jsApiTicketStore, IDistributedCache<WeChatPayAuthenticationStateCacheItem> stateCache)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
            _accessTokenStore = accessTokenStore;
            _jsApiTicketStore = jsApiTicketStore;
            _stateCache = stateCache;
        }

        /// <summary>根据JsCode获取用户OpenId和UnionId
        /// </summary>
        /// <param name="appId">应用AppId</param>
        /// <param name="appSecret">应用AppSecret</param>
        /// <param name="jsCode">应用的JsCode</param>
        /// <returns></returns>
        public virtual async Task<MiniProgramOpenIdResponse> GetMiniProgramOpenId(string appId, string appSecret, string jsCode)
        {
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={jsCode}&grant_type=authorization_code";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"获取微信小程序OpenId失败,请求url地址:[{url}],返回Http状态:{response.StatusCode}");
            }
            var responseString =await  response.Content.ReadAsStringAsync();
           
            //记录日志
            _logger.LogInformation(WeChatPayUtil.ParseLog($"获取小程序用户OpenId返回结果,{responseString}"));
            var miniProgramOpenIdResponse = _jsonSerializer.Deserialize<MiniProgramOpenIdResponse>(responseString);
            return miniProgramOpenIdResponse;
        }

        /// <summary>获取用户Code的Url地址
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="redirectUri">回调地址</param>
        /// <param name="scope">权限</param>
        /// <param name="state">附加状态(a-zA-Z0-9的参数值，最多128字节,如果不为空必须唯一)</param>
        /// <returns></returns>
        public virtual string GetAuthorizationCodeUrl(string appId, string redirectUri, string scope = WeChatPaySettings.Scope.Base, string state = "")
        {
            if (state.IsNullOrWhiteSpace())
            {
                state = "STATE";
            }
            else
            {
                _stateCache.Set(state, new WeChatPayAuthenticationStateCacheItem(state), new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
            var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={WebUtility.UrlEncode(redirectUri)}&response_type=code&scope={scope}&state={state}#weChat_redirect";
            return url;
        }

        /// <summary>获取用户的OpenId
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <param name="code">微信返回的用户Code</param>
        /// <param name="verifyStatus">是否验证状态</param>
        /// <param name="state">前一步提交的状态</param>
        /// <returns></returns>
        public virtual async Task<string> GetUserOpenIdAsync(string appId, string appSecret, string code, bool verifyStatus = false, string state = "")
        {
            if (verifyStatus)
            {
                if (!state.IsNullOrWhiteSpace())
                {
                    var weChatPayAuthenticationStateCacheItem = await _stateCache.GetAsync(state);
                    if (weChatPayAuthenticationStateCacheItem == null)
                    {
                        _logger.LogError(WeChatPayUtil.ParseLog("JsApi认证验证Status失败,Status不存在"));
                        throw new Exception($"微信Status验证不通过");
                    }
                    if (weChatPayAuthenticationStateCacheItem.State != state)
                    {
                        _logger.LogError(WeChatPayUtil.ParseLog("JsApi认证验证Status失败,Status不匹配"));
                    }
                }
            }

            var url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={appId}&secret={appSecret}&code={code}&grant_type=authorization_code";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"获取微信用户OpenId失败,请求url地址:[{url}],返回Http状态:{response.StatusCode}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            //记录日志
            _logger.LogInformation(WeChatPayUtil.ParseLog($"获取用户OpenId返回结果,{responseString}"));
            var getUserOpenIdResponse = _jsonSerializer.Deserialize<UserOpenIdResponse>(responseString);
            return getUserOpenIdResponse.OpenId;
        }






    }
}
