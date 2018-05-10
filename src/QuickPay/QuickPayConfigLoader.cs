using DotCommon.Serializing;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
using System.IO;
using System.Xml;

namespace QuickPay
{
    public class QuickPayConfigLoader
    {
        private readonly IJsonSerializer _jsonSerializer;
        public QuickPayConfigLoader(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>读取支付宝和微信配置
        /// </summary>
        public ConfigWapper LoadConfigWapper(string file, string format = QuickPaySettings.ConfigFormat.Json)
        {
            //检测配置文件是否存在
            if (!File.Exists(file))
            {
                return default(ConfigWapper);
            }
            var txt = File.ReadAllText(file).Trim();
            if (format == QuickPaySettings.ConfigFormat.Json)
            {
                return _jsonSerializer.Deserialize<ConfigWapper>(txt);
            }
            return LoadFromXml(file);
        }

        private ConfigWapper LoadFromXml(string file)
        {
            var configWapper = new ConfigWapper();
            var doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true//忽略文档里面的注释
            };
            XmlReader reader = XmlReader.Create(file, settings);
            doc.Load(reader);
            XmlNode root = doc.SelectSingleNode("QuickPayConfig");
            var alipayNode = root.SelectSingleNode("Alipay");
            configWapper.AlipayConfig = new AlipayConfig()
            {
                DefaultAppName = alipayNode.SelectSingleNode("DefaultAppName").InnerText,
                NotifyGateway = alipayNode.SelectSingleNode("NotifyGateway").InnerText,
                LocalAddress = alipayNode.SelectSingleNode("LocalAddress").InnerText,
                WebGateway = alipayNode.SelectSingleNode("WebGateway").InnerText,
                Gateway = alipayNode.SelectSingleNode("Gateway").InnerText,
                Format = alipayNode.SelectSingleNode("Format").InnerText,
                Version = alipayNode.SelectSingleNode("Version").InnerText,
                NotifyRealateUrl = alipayNode.SelectSingleNode("NotifyRealateUrl").InnerText,
                QrcodeNotifyRelateUrl = alipayNode.SelectSingleNode("QrcodeNotifyRelateUrl").InnerText,
                BarcodeNotifyRelateUrl = alipayNode.SelectSingleNode("BarcodeNotifyRelateUrl").InnerText,
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

            var wxpayNode = root.SelectSingleNode("WechatPay");
            configWapper.WechatPayConfig = new WechatPayConfig()
            {
                DefaultAppName = wxpayNode.SelectSingleNode("DefaultAppName").InnerText,
                NotifyGateway = wxpayNode.SelectSingleNode("NotifyGateway").InnerText,
                LocalAddress = wxpayNode.SelectSingleNode("LocalAddress").InnerText,
                WebGateway = wxpayNode.SelectSingleNode("WebGateway").InnerText,
                NotifyRealateUrl = wxpayNode.SelectSingleNode("NotifyRealateUrl").InnerText,
                SignType = wxpayNode.SelectSingleNode("SignType").InnerText,
                SslPassword = wxpayNode.SelectSingleNode("SslPassword").InnerText,
            };

            //WxpayConfig Apps节点
            var wxpayAppNodes = wxpayNode.SelectSingleNode("Apps");
            foreach (XmlNode wxpayAppNode in wxpayAppNodes)
            {
                var wechatPayApp = new WechatPayApp()
                {
                    AppId = wxpayAppNode.SelectSingleNode("AppId").InnerText,
                    AppTypeId = int.Parse(wxpayAppNode.SelectSingleNode("AppTypeId").InnerText),
                    Name = wxpayAppNode.SelectSingleNode("Name").InnerText,
                    MchId = wxpayAppNode.SelectSingleNode("MchId").InnerText,
                    Key = wxpayAppNode.SelectSingleNode("Key").InnerText,
                    Appsecret = wxpayAppNode.SelectSingleNode("Appsecret").InnerText,
                };
                var nativeMobileInfoNode = wxpayAppNode.SelectSingleNode("NativeMobileInfo");
                wechatPayApp.NativeMobileInfo = new NativeMobileInfo()
                {
                    IosName = nativeMobileInfoNode.SelectSingleNode("IosName").InnerText,
                    BundleId = nativeMobileInfoNode.SelectSingleNode("BundleId").InnerText,
                    AndroidName = nativeMobileInfoNode.SelectSingleNode("AndroidName").InnerText,
                    PackageName = nativeMobileInfoNode.SelectSingleNode("PackageName").InnerText,
                    WapName = nativeMobileInfoNode.SelectSingleNode("WapName").InnerText,
                    WapUrl = nativeMobileInfoNode.SelectSingleNode("WapUrl").InnerText,
                };
                configWapper.WechatPayConfig.Apps.Add(wechatPayApp);
            }
            return configWapper;
        }


    }
}
