using DotCommon.Extensions;
using DotCommon.Utility;
using QuickPay.Infrastructure.Apps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Alipay.Apps
{
    /// <summary>支付宝相关配置
    /// </summary>
    public class AlipayConfig : QuickPayConfig
    {
        /// <summary>默认的应用名
        /// </summary>
        public string DefaultAppName { get; set; }

        /// <summary>支付宝支付异步通知默认网关地址
        /// </summary>
        public string NotifyGateway { get; set; }

        /// <summary>本地IP地址
        /// </summary>
        public string LocalAddress { get; set; }

        /// <summary>网址(如果前后端分离,为前端的网址)
        /// </summary>
        public string WebGateway { get; set; }

        /// <summary>异步通知关联Url
        /// </summary>
        public string NotifyUrlFragments { get; set; }

        /// <summary>二维码扫码支付异步通知关联Url
        /// </summary>
        public string QrcodeNotifyUrlFragments { get; set; }

        /// <summary>条码支付异步通知关联Url
        /// </summary>
        public string BarcodeNotifyUrlFragments { get; set; }

        /// <summary>支付宝网关
        /// </summary>
        public string Gateway { get; set; } = "https://openapi.alipay.com/gateway.do";

        /// <summary>沙箱网关
        /// </summary>
        public string SandboxGateway { get; set; } = "https://openapi.alipaydev.com/gateway.do";

        /// <summary>格式--JSON
        /// </summary>
        public string Format { get; set; } = "JSON";

        /// <summary>调用的接口版本，固定为：1.0
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>支付宝应用
        /// </summary>

        public List<AlipayApp> Apps = new List<AlipayApp>();

        /// <summary>根据名称获取支付宝
        /// </summary>
        public AlipayApp GetByName(string name)
        {
            return Apps.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>根据AppId获取支付宝
        /// </summary>
        public AlipayApp GetByAppId(string appId)
        {
            return Apps.FirstOrDefault(x => x.AppId == appId);
        }

        /// <summary>获取默认支付宝App
        /// </summary>
        public AlipayApp GetDefaultApp()
        {
            if (!DefaultAppName.IsNullOrWhiteSpace())
            {
                return Apps.First(x => x.Name == DefaultAppName);
            }
            throw new ArgumentException($"DefaultAppName 未配置!");
        }

        /// <summary>获取默认通知地址
        /// </summary>
        public string GetDefaultNotifyUrl()
        {
            return UrlUtil.CombineUrl(NotifyGateway, NotifyUrlFragments);
        }

        /// <summary>获取默认扫码支付通知地址
        /// </summary>
        public string GetDefaultQrcodeNotifyUrl()
        {
            return UrlUtil.CombineUrl(NotifyGateway, QrcodeNotifyUrlFragments);
        }

        /// <summary>获取默认条码支付通知地址
        /// </summary>
        public string GetDefaultBarcodeNotifyUrl()
        {
            return UrlUtil.CombineUrl(NotifyGateway, BarcodeNotifyUrlFragments);
        }

        /// <summary>Copy
        /// </summary>
        public AlipayConfig SelfCopy(AlipayConfig alipayConfig)
        {
            DefaultAppName = alipayConfig.DefaultAppName;
            NotifyGateway = alipayConfig.NotifyGateway;
            LocalAddress = alipayConfig.LocalAddress;
            WebGateway = alipayConfig.WebGateway;
            NotifyUrlFragments = alipayConfig.NotifyUrlFragments;
            QrcodeNotifyUrlFragments = alipayConfig.QrcodeNotifyUrlFragments;
            BarcodeNotifyUrlFragments = alipayConfig.BarcodeNotifyUrlFragments;
            Gateway = alipayConfig.Gateway;
            Format = alipayConfig.Format;
            Version = alipayConfig.Version;
            Apps.Clear();
            foreach (var app in alipayConfig.Apps)
            {
                Apps.Add(new AlipayApp()
                {
                    Name = app.Name,
                        AppId = app.AppId,
                        Charset = app.Charset,
                        SignType = app.SignType,
                        PublicKey = app.PublicKey,
                        PrivateKey = app.PrivateKey,
                        AppTypeId = app.AppTypeId,
                        EnableEncrypt = app.EnableEncrypt,
                        EncryptType = app.EncryptType,
                        EncryptKey = app.EncryptKey
                });
            }

            return this;
        }
    }
}
