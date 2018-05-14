﻿using DotCommon.Dependency;
using DotCommon.Extensions;
using DotCommon.Http;
using DotCommon.Logging;
using DotCommon.Runtime.Caching;
using DotCommon.Serializing;
using QuickPay.WechatPay.Util;
using System;
using System.Net;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private ILogger Logger { get; }
        private readonly IHttpClient _httpClient;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IAccessTokenStore _accessTokenStore;
        private readonly IJsApiTicketStore _jsApiTicketStore;
        private readonly ICacheManager _cacheManager;

        public AuthenticationService(IHttpClient httpClient, IJsonSerializer jsonSerializer, IAccessTokenStore accessTokenStore, IJsApiTicketStore jsApiTicketStore, ICacheManager cacheManager)
        {
            _httpClient = httpClient;
            _jsonSerializer = jsonSerializer;
            _accessTokenStore = accessTokenStore;
            _jsApiTicketStore = jsApiTicketStore;
            _cacheManager = cacheManager;
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }

        /// <summary>获取用户Code的Url地址
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="redirectUri">回调地址</param>
        /// <param name="scope">权限</param>
        /// <param name="state">附加状态(a-zA-Z0-9的参数值，最多128字节,如果不为空必须唯一)</param>
        /// <returns></returns>
        public virtual string GetAuthorizationCodeUrl(string appId, string redirectUri, string scope = WechatPaySettings.Scope.Base, string state = "")
        {
            if (state.IsNullOrWhiteSpace())
            {
                state = "STATE";
            }
            else
            {
                //state = ObjectId.GenerateNewStringId();
                _cacheManager.GetCache(WechatPaySettings.AuthenticationState).Set(state, state, TimeSpan.FromMinutes(5));
            }
            var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={WebUtility.UrlEncode(redirectUri)}&response_type=code&scope={scope}&state={state}#wechat_redirect";
            return url;
        }

        /// <summary>获取用户的OpenId
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <param name="code">微信返回的用户Code</param>
        /// <param name="verifyStatus">是否验证状态</param>
        /// <param name="status">前一步提交的状态</param>
        /// <returns></returns>
        public virtual async Task<string> GetUserOpenIdAsync(string appId, string appSecret, string code, bool verifyStatus = false, string status = "")
        {
            if (verifyStatus)
            {
                if (!status.IsNullOrWhiteSpace())
                {
                    var cacheStatus = _cacheManager.GetCache<string, string>(WechatPaySettings.AuthenticationState).Get(status, () => "");
                    if (cacheStatus.IsNullOrWhiteSpace())
                    {
                        Logger.Error(WechatPayUtil.ParseLog("JsApi认证验证Status失败,Status不存在"));
                        throw new Exception($"微信Status验证不通过");
                    }
                    if (cacheStatus != status)
                    {
                        Logger.Error(WechatPayUtil.ParseLog("JsApi认证验证Status失败,Status不匹配"));
                    }
                }
            }

            var url = $"https://api.weixin.qq.com/sns/oauth2/access_token";
            var builder = RequestBuilder.Instance(url, RequestConsts.Methods.Get)
                .AttachParam("appId", appId)
                .AttachParam("secret", appSecret)
                .AttachParam("code", code)
                .AttachParam("grant_type", "authorization_code");
            var response = await _httpClient.ExecuteAsync(builder);
            //记录日志
            Logger.Info(WechatPayUtil.ParseLog($"获取用户OpenId返回结果,{response.GetResponseString()}"));
            var getUserOpenIdResponse = _jsonSerializer.Deserialize<UserOpenIdResponse>(response.GetResponseString());
            return getUserOpenIdResponse.OpenId;
        }


        /// <summary>从微信接口获取公众号AccessToken
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        public virtual async Task<AccessTokenResponse> GetRemoteAccessTokenAsync(string appId, string appSecret)
        {
            var getAccessTokenResponse = await GetAccessTokenInternal(appId, appSecret);
            return getAccessTokenResponse;
        }

        /// <summary>获取可用的AccessToken(先从本地存储中获取,如果不存在,就从微信接口获取)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        public virtual async Task<string> GetAccessTokenAsync(string appId, string appSecret)
        {
            //从存储中读取AccessToken
            var accessTokenInfo = await _accessTokenStore.GetAccessTokenAsync(appId);
            if (accessTokenInfo == null || (accessTokenInfo != null && accessTokenInfo.IsExpired(DateTime.Now)))
            {
                //从微信获取AccessToken
                var getAccessTokenResponse = await GetRemoteAccessTokenAsync(appId, appSecret);
                accessTokenInfo = new AccessToken()
                {
                    AppId = appId,
                    Token = getAccessTokenResponse.AccessToken,
                    ExpiredIn = getAccessTokenResponse.ExpiresIn,
                    LastModifiedTime = DateTime.Now
                };
                //更新存储中的AccessToken
                await _accessTokenStore.CreateOrUpdateAccessTokenAsync(accessTokenInfo);
                return getAccessTokenResponse.AccessToken;
            }
            return accessTokenInfo.Token;
        }

        /// <summary>从微信接口获取微信JsApiTicket
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        public virtual async Task<JsApiTicketResponse> GetRemoteJsApiTicketAsync(string appId, string appSecret)
        {
            //获取公众号的AccessToken
            var accessToken = await GetAccessTokenAsync(appId, appSecret);
            return (await GetJsApiTicketInternal(appId, accessToken));
        }

        /// <summary>获取可用的微信JsApiTicket(先从本地存储中获取,如果本地存储不存在就从微信接口获取)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密码</param>
        /// <returns></returns>
        public virtual async Task<string> GetJsApiTicketAsync(string appId, string appSecret)
        {
            //从存储中读取JsApiTicket
            var jsApiTicketInfo = await _jsApiTicketStore.GetJsApiTicketAsync(appId);
            if (jsApiTicketInfo == null || (jsApiTicketInfo != null && jsApiTicketInfo.IsExpired(DateTime.Now)))
            {
                //从微信获取JsApiTicket
                var getJsApiTicketResponse = await GetRemoteJsApiTicketAsync(appId, appSecret);
                jsApiTicketInfo = new JsApiTicket()
                {
                    AppId = appId,
                    Ticket = getJsApiTicketResponse.Ticket,
                    ExpiredIn = getJsApiTicketResponse.ExpiresIn,
                    LastModifiedTime = DateTime.Now
                };
                //更新存储中的JsApiTicket
                await _jsApiTicketStore.CreateOrUpdateJsApiTicketAsync(jsApiTicketInfo);
                return getJsApiTicketResponse.Ticket;
            }
            return jsApiTicketInfo.Ticket;
        }


        #region Utilities
        /// <summary>获取AccessToken
        /// </summary>
        protected virtual async Task<AccessTokenResponse> GetAccessTokenInternal(string appId, string appSecret)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/token";
            var builder = RequestBuilder.Instance(url, RequestConsts.Methods.Get)
                .AttachParam("grant_type", "client_credential")
                .AttachParam("appid", appId)
                .AttachParam("secret", appSecret);
            var response = await _httpClient.ExecuteAsync(builder);
            //记录日志
            Logger.Info(WechatPayUtil.ParseLog($"获取公众号AccessToekn返回结果,{response.GetResponseString()}"));
            var getAccessTokenResponse = _jsonSerializer.Deserialize<AccessTokenResponse>(response.GetResponseString());
            //如果返回的不是有效的accessToken的json格式,代表出错了
            if (getAccessTokenResponse.AccessToken.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("获取公众号AccessToken失败");
            }
            return getAccessTokenResponse;
        }

        /// <summary>获取微信支付JsApiTicket
        /// </summary>
        protected virtual async Task<JsApiTicketResponse> GetJsApiTicketInternal(string appId, string accessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket";
            var builder = RequestBuilder.Instance(url, RequestConsts.Methods.Get)
                .AttachParam("access_token", accessToken)
                .AttachParam("type", WechatPaySettings.TradeType.JsApi);
            var response = await _httpClient.ExecuteAsync(builder);
            //记录日志
            Logger.Info(WechatPayUtil.ParseLog($"获取公众号JsApi_Ticket返回结果,{response.GetResponseString()}"));
            var getJsApiTicketResponse = _jsonSerializer.Deserialize<JsApiTicketResponse>(response.GetResponseString());
            if (getJsApiTicketResponse.Ticket.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("获取JsApiTicket失败");
            }
            return getJsApiTicketResponse;
        }

        #endregion


    }
}