﻿using DotCommon.Extensions;
using DotCommon.Utility;
using QuickPay.Infrastructure.Apps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.WeChatPay.Apps
{
    /// <summary>微信支付配置
    /// </summary>
    public class WeChatPayConfig : QuickPayConfig
    {
        /// <summary>默认的应用名
        /// </summary>
        public string DefaultAppId { get; set; }

        /// <summary>支付宝网关
        /// </summary>
        public string Gateway { get; set; } = WeChatPaySettings.Urls.Gateway;

        /// <summary>沙箱网关
        /// </summary>
        public string SandboxGateway { get; set; } = WeChatPaySettings.Urls.SandboxGateway;

        /// <summary>微信支付异步通知默认网关地址
        /// </summary>
        public string NotifyGateway { get; set; }

        /// <summary>异步通知关联Url
        /// </summary>
        public string NotifyUrlFragments { get; set; }

        /// <summary>本地IP地址
        /// </summary>
        public string LocalAddress { get; set; }

        /// <summary>网址(如果前后端分离,为前端的网址)
        /// </summary>
        public string WebGateway { get; set; }

        /// <summary>签名类型
        /// </summary>
        public string SignType { get; set; } = WeChatPaySettings.SignType.Md5;

        /// <summary>SSL证书
        /// </summary>
        public string SslPassword { get; set; }

        /// <summary>应用
        /// </summary>
        public List<WeChatPayApp> Apps = new List<WeChatPayApp>();

        /// <summary>根据名称获取应用
        /// </summary>
        public WeChatPayApp GetByName(string name)
        {
            return Apps.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>根据AppId获取应用
        /// </summary>
        public WeChatPayApp GetByAppId(string appId)
        {
            return Apps.FirstOrDefault(x => x.AppId == appId);
        }

        /// <summary>获取默认应用
        /// </summary>
        public WeChatPayApp GetDefaultApp()
        {
            if (!DefaultAppId.IsNullOrWhiteSpace())
            {
                var app = Apps.FirstOrDefault(x => x.AppId == DefaultAppId);
                if (app != null)
                {
                    return app;
                }
            }
            throw new ArgumentException($"DefaultAppName 未配置!");
        }

        /// <summary>获取默认异步通知地址
        /// </summary>
        public string GetDefaultNotifyUrl()
        {
            return UrlUtil.CombineUrl(NotifyGateway, NotifyUrlFragments);
        }


        /// <summary>Copy
        /// </summary>
        public WeChatPayConfig SelfCopy(WeChatPayConfig weChatPayConfig)
        {
            DefaultAppId = weChatPayConfig.DefaultAppId;
            NotifyGateway = weChatPayConfig.NotifyGateway;
            NotifyUrlFragments = weChatPayConfig.NotifyUrlFragments;
            LocalAddress = weChatPayConfig.LocalAddress;
            WebGateway = weChatPayConfig.WebGateway;
            SignType = weChatPayConfig.SignType;
            SslPassword = weChatPayConfig.SslPassword;
            Apps.Clear();
            foreach (var app in weChatPayConfig.Apps)
            {
                Apps.Add(new WeChatPayApp()
                {
                    Name = app.Name,
                        AppId = app.AppId,
                        MchId = app.MchId,
                        Key = app.Key,
                        Appsecret = app.Appsecret,
                        AppTypeId = app.AppTypeId,
                        NativeMobileInfo = new NativeMobileInfo()
                        {
                            AndroidName = app.NativeMobileInfo.AndroidName,
                                PackageName = app.NativeMobileInfo.PackageName,
                                BundleId = app.NativeMobileInfo.BundleId,
                                IosName = app.NativeMobileInfo.IosName,
                                WapName = app.NativeMobileInfo.WapName,
                                WapUrl = app.NativeMobileInfo.WapUrl
                        }
                });
            }
            return this;
        }
    }
}
