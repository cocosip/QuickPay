using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Alipay.Apps
{
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
        public string NotifyRealateUrl { get; set; }

        /// <summary>二维码扫码支付异步通知关联Url
        /// </summary>
        public string QrcodeNotifyRelateUrl { get; set; }

        /// <summary>条码支付异步通知关联Url
        /// </summary>
        public string BarcodeNotifyRelateUrl { get; set; }

        /// <summary>支付宝网关
        /// </summary>
        public string Gateway { get; set; } = "https://openapi.alipay.com/gateway.do";

        /// <summary>格式--JSON
        /// </summary>
        public string Format { get; set; } = "JSON";

        /// <summary>调用的接口版本，固定为：1.0
        /// </summary>
        public string Version { get; set; } = "1.0";

        public List<AlipayApp> Apps = new List<AlipayApp>();
        public AlipayApp GetByName(string name)
        {
            return Apps.FirstOrDefault(x => x.Name == name);
        }

        public AlipayApp GetByAppId(string appId)
        {
            return Apps.FirstOrDefault(x => x.AppId == appId);
        }

        public AlipayApp GetDefaultApp()
        {
            if (!DefaultAppName.IsNullOrWhiteSpace())
            {
                return Apps.First(x => x.Name == DefaultAppName);
            }
            throw new ArgumentException($"DefaultAppName 未配置!");
        }


        public string GetDefaultNotifyUrl()
        {
            return $"{NotifyGateway.TrimEnd('/')}/{NotifyRealateUrl.TrimStart('/')}";
        }

        public string GetDefaultQrcodeNotifyUrl()
        {
            return $"{NotifyGateway.TrimEnd('/')}/{QrcodeNotifyRelateUrl.TrimStart('/')}";
        }
        public string GetDefaultBarcodeNotifyUrl()
        {
            return $"{NotifyGateway.TrimEnd('/')}/{BarcodeNotifyRelateUrl.TrimStart('/')}";
        }

        public AlipayConfig SelfCopy(AlipayConfig alipayConfig)
        {
            DefaultAppName = alipayConfig.DefaultAppName;
            NotifyGateway = alipayConfig.NotifyGateway;
            LocalAddress = alipayConfig.LocalAddress;
            WebGateway = alipayConfig.WebGateway;
            NotifyRealateUrl = alipayConfig.NotifyRealateUrl;
            QrcodeNotifyRelateUrl = alipayConfig.QrcodeNotifyRelateUrl;
            BarcodeNotifyRelateUrl = alipayConfig.BarcodeNotifyRelateUrl;
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
