using QuickPay.Alipay.Apps;
using QuickPay.WeChatPay.Apps;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace QuickPay.Configurations
{
    /// <summary>配置文件帮助类
    /// </summary>
    public static class ConfigurationFileHelper
    {
        /// <summary>读取支付宝和微信配置
        /// </summary>
        public static ConfigWrapper TranslateToConfigWrapper(string file)
        {
            //检测配置文件是否存在
            if (!File.Exists(file))
            {
                throw new ArgumentNullException($"未找到支付配置文件:{file}");
            }
            return LoadFromXml(file);
        }

        /// <summary>将配置转换成文件内容
        /// </summary>
        public static string TranslateToText(ConfigWrapper configWrapper)
        {


            var output = new StringBuilder();
            output.AppendLine("<QuickPayConfig>");
            //支付宝
            output.Append(BuildAlipayXml(configWrapper.AlipayConfig));
            //微信
            output.Append(BuildWeChatXml(configWrapper.WeChatPayConfig));
            output.AppendLine("</QuickPayConfig>");
            return output.ToString();

        }


        /// <summary>从文件中加载配置包装,ConfigWrapper
        /// </summary>
        private static ConfigWrapper LoadFromXml(string file)
        {
            var configWapper = new ConfigWrapper();
            var doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true //忽略文档里面的注释
            };
            XmlReader reader = XmlReader.Create(file, settings);
            doc.Load(reader);
            XmlNode root = doc.SelectSingleNode("QuickPayConfig");
            var alipayNode = root.SelectSingleNode("Alipay");
            configWapper.AlipayConfig = new AlipayConfig()
            {
                Id = alipayNode.SelectSingleNode("Id").InnerText,
                DefaultAppName = alipayNode.SelectSingleNode("DefaultAppName").InnerText,
                NotifyGateway = alipayNode.SelectSingleNode("NotifyGateway").InnerText,
                LocalAddress = alipayNode.SelectSingleNode("LocalAddress").InnerText,
                WebGateway = alipayNode.SelectSingleNode("WebGateway").InnerText,
                Gateway = alipayNode.SelectSingleNode("Gateway").InnerText,
                SandboxGateway = alipayNode.SelectSingleNode("SandboxGateway").InnerText,
                Format = alipayNode.SelectSingleNode("Format").InnerText,
                Version = alipayNode.SelectSingleNode("Version").InnerText,
                NotifyUrlFragments = alipayNode.SelectSingleNode("NotifyUrlFragments").InnerText,
                QrcodeNotifyUrlFragments = alipayNode.SelectSingleNode("QrcodeNotifyUrlFragments").InnerText,
                BarcodeNotifyUrlFragments = alipayNode.SelectSingleNode("BarcodeNotifyUrlFragments").InnerText,
            };
            //AlipayConfig Apps节点
            var alipayAppNodes = alipayNode.SelectSingleNode("Apps");
            foreach (XmlNode alipayAppNode in alipayAppNodes)
            {
                var alipayApp = new AlipayApp()
                {
                    AppId = alipayAppNode.SelectSingleNode("AppId").InnerText,
                    AppTypeId = int.Parse(alipayAppNode.SelectSingleNode("AppTypeId").InnerText),
                    Charset = alipayAppNode.SelectSingleNode("Charset").InnerText,
                    Name = alipayAppNode.SelectSingleNode("Name").InnerText,
                    PrivateKey = alipayAppNode.SelectSingleNode("PrivateKey").InnerText,
                    PublicKey = alipayAppNode.SelectSingleNode("PublicKey").InnerText,
                    SignType = alipayAppNode.SelectSingleNode("SignType").InnerText,
                    EnableEncrypt = bool.Parse(alipayAppNode.SelectSingleNode("EnableEncrypt").InnerText),
                    EncryptType = alipayAppNode.SelectSingleNode("EncryptType").InnerText,
                    EncryptKey = alipayAppNode.SelectSingleNode("EncryptKey").InnerText
                };
                configWapper.AlipayConfig.Apps.Add(alipayApp);
            }

            var weChatPayNode = root.SelectSingleNode("WeChatPay");
            configWapper.WeChatPayConfig = new WeChatPayConfig()
            {
                Id = weChatPayNode.SelectSingleNode("Id").InnerText,
                DefaultAppName = weChatPayNode.SelectSingleNode("DefaultAppName").InnerText,
                NotifyGateway = weChatPayNode.SelectSingleNode("NotifyGateway").InnerText,
                LocalAddress = weChatPayNode.SelectSingleNode("LocalAddress").InnerText,
                WebGateway = weChatPayNode.SelectSingleNode("WebGateway").InnerText,
                NotifyUrlFragments = weChatPayNode.SelectSingleNode("NotifyUrlFragments").InnerText,
                SignType = weChatPayNode.SelectSingleNode("SignType").InnerText,
                SslPassword = weChatPayNode.SelectSingleNode("SslPassword").InnerText,
            };

            //weChatPayConfig Apps节点
            var weChatPayAppNodes = weChatPayNode.SelectSingleNode("Apps");
            foreach (XmlNode weChatPayAppNode in weChatPayAppNodes)
            {
                var weChatPayApp = new WeChatPayApp()
                {
                    AppId = weChatPayAppNode.SelectSingleNode("AppId").InnerText,
                    AppTypeId = int.Parse(weChatPayAppNode.SelectSingleNode("AppTypeId").InnerText),
                    Name = weChatPayAppNode.SelectSingleNode("Name").InnerText,
                    MchId = weChatPayAppNode.SelectSingleNode("MchId").InnerText,
                    Key = weChatPayAppNode.SelectSingleNode("Key").InnerText,
                    Appsecret = weChatPayAppNode.SelectSingleNode("Appsecret").InnerText,
                };
                var nativeMobileInfoNode = weChatPayAppNode.SelectSingleNode("NativeMobileInfo");
                weChatPayApp.NativeMobileInfo = new NativeMobileInfo()
                {
                    IosName = nativeMobileInfoNode.SelectSingleNode("IosName").InnerText,
                    BundleId = nativeMobileInfoNode.SelectSingleNode("BundleId").InnerText,
                    AndroidName = nativeMobileInfoNode.SelectSingleNode("AndroidName").InnerText,
                    PackageName = nativeMobileInfoNode.SelectSingleNode("PackageName").InnerText,
                    WapName = nativeMobileInfoNode.SelectSingleNode("WapName").InnerText,
                    WapUrl = nativeMobileInfoNode.SelectSingleNode("WapUrl").InnerText,
                };
                configWapper.WeChatPayConfig.Apps.Add(weChatPayApp);
            }
            //关闭xml读取流,避免新配置文件无法覆盖
            reader.Close();
            return configWapper;
        }

        /// <summary>构建支付宝配置xml字符串
        /// </summary>
        private static string BuildAlipayXml(AlipayConfig alipayConfig)
        {
            var output = new StringBuilder();

            output.AppendLine("<Alipay>");
            output.AppendFormat("<Gateway>{0}", alipayConfig.Gateway);
            output.AppendLine("</Gateway>");

            output.AppendFormat("<SandboxGateway>{0}", alipayConfig.SandboxGateway);
            output.AppendLine("</SandboxGateway>");
            output.AppendFormat("<LocalAddress>{0}", alipayConfig.LocalAddress);
            output.AppendLine("</LocalAddress>");

            output.AppendFormat("<WebGateway>{0}", alipayConfig.WebGateway);
            output.AppendLine("</WebGateway>");

            output.AppendFormat("<NotifyGateway>{0}", alipayConfig.NotifyGateway);
            output.AppendLine("</NotifyGateway>");

            output.AppendFormat("<NotifyUrlFragments>{0}", alipayConfig.NotifyUrlFragments);
            output.AppendLine("</NotifyUrlFragments>");

            output.AppendFormat("<QrcodeNotifyUrlFragments>{0}", alipayConfig.QrcodeNotifyUrlFragments);
            output.AppendLine("</QrcodeNotifyUrlFragments>");

            output.AppendFormat("<BarcodeNotifyUrlFragments>{0}", alipayConfig.BarcodeNotifyUrlFragments);
            output.AppendLine("</BarcodeNotifyUrlFragments>");

            output.AppendFormat("<Format>{0}", alipayConfig.Format);
            output.AppendLine("</Format>");

            output.AppendFormat("<Version>{0}", alipayConfig.Version);
            output.AppendLine("</Version>");

            output.AppendFormat("<DefaultAppName>{0}", alipayConfig.DefaultAppName);
            output.AppendLine("</DefaultAppName>");

            output.AppendLine("<Apps>");
            foreach (var app in alipayConfig.Apps)
            {
                output.AppendLine("<AlipayApp>");
                output.AppendFormat("<Name>{0}", app.Name);
                output.AppendLine("</Name>");

                output.AppendFormat("<AppId>{0}", app.AppId);
                output.AppendLine("</AppId>");

                output.AppendFormat("<AppTypeId>{0}", app.AppTypeId);
                output.AppendLine("</AppTypeId>");

                output.AppendFormat("<Charset>{0}", app.Charset);
                output.AppendLine("</Charset>");

                output.AppendFormat("<PrivateKey>{0}", app.PrivateKey);
                output.AppendLine("</PrivateKey>");

                output.AppendFormat("<PublicKey>{0}", app.PublicKey);
                output.AppendLine("</PublicKey>");

                output.AppendFormat("<SignType>{0}", app.SignType);
                output.AppendLine("</SignType>");

                output.AppendFormat("<EnableEncrypt>{0}", app.EnableEncrypt);
                output.AppendLine("</EnableEncrypt>");

                output.AppendFormat("<EncryptType>{0}", app.EncryptType);
                output.AppendLine("</EncryptType>");

                output.AppendFormat("<EncryptKey>{0}", app.EncryptKey);
                output.AppendLine("</EncryptKey>");

                output.AppendLine("</AlipayApp>");
            }

            output.AppendLine("</Apps>");
            output.AppendLine("</Alipay>");

            return output.ToString();
        }

        /// <summary>构建微信支付配置xml字符串
        /// </summary>
        private static string BuildWeChatXml(WeChatPayConfig weChatPayConfig)
        {
            var output = new StringBuilder();

            output.AppendLine("<WeChatPay>");

            output.AppendFormat("<LocalAddress>{0}", weChatPayConfig.LocalAddress);
            output.AppendLine("</LocalAddress>");

            output.AppendFormat("<WebGateway>{0}", weChatPayConfig.WebGateway);
            output.AppendLine("</WebGateway>");

            output.AppendFormat("<NotifyGateway>{0}", weChatPayConfig.NotifyGateway);
            output.AppendLine("</NotifyGateway>");

            output.AppendFormat("<NotifyUrlFragments>{0}", weChatPayConfig.NotifyUrlFragments);
            output.AppendLine("</NotifyUrlFragments>");

            output.AppendFormat("<DefaultAppName>{0}", weChatPayConfig.DefaultAppName);
            output.AppendLine("</DefaultAppName>");

            output.AppendFormat("<SignType>{0}", weChatPayConfig.SignType);
            output.AppendLine("</SignType>");

            output.AppendFormat("<SslPassword>{0}", weChatPayConfig.SslPassword);
            output.AppendLine("</SslPassword>");

            output.AppendLine("<Apps>");
            foreach (var app in weChatPayConfig.Apps)
            {
                output.AppendLine("<WeChatPayApp>");
                output.AppendFormat("<Name>{0}", app.Name);
                output.AppendLine("</Name>");

                output.AppendFormat("<AppId>{0}", app.AppId);
                output.AppendLine("</AppId>");

                output.AppendFormat("<MchId>{0}", app.MchId);
                output.AppendLine("</MchId>");

                output.AppendFormat("<AppTypeId>{0}", app.AppTypeId);
                output.AppendLine("</AppTypeId>");

                output.AppendFormat("<Appsecret>{0}", app.Appsecret);
                output.AppendLine("</Appsecret>");

                output.AppendFormat("<Key>{0}", app.Key);
                output.AppendLine("</Key>");

                output.AppendLine("<NativeMobileInfo>");
                var mobileInfo = app.NativeMobileInfo;
                output.AppendFormat("<BundleId>{0}", mobileInfo.BundleId);
                output.AppendLine("</BundleId>");

                output.AppendFormat("<IosName>{0}", mobileInfo.IosName);
                output.AppendLine("</IosName>");

                output.AppendFormat("<AndroidName>{0}", mobileInfo.AndroidName);
                output.AppendLine("</AndroidName>");

                output.AppendFormat("<PackageName>{0}", mobileInfo.PackageName);
                output.AppendLine("</PackageName>");

                output.AppendFormat("<WapName>{0}", mobileInfo.WapName);
                output.AppendLine("</WapName>");

                output.AppendFormat("<WapUrl>{0}", mobileInfo.WapUrl);
                output.AppendLine("</WapUrl>");

                output.AppendLine("</NativeMobileInfo>");

                output.AppendLine("</WeChatPayApp>");
            }

            output.AppendLine("</Apps>");

            output.AppendLine("</WeChatPay>");

            return output.ToString();
        }
    }
}
