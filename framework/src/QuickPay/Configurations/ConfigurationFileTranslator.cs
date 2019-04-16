using DotCommon.Serializing;
using QuickPay.Alipay.Apps;
using QuickPay.WeChatPay.Apps;
using System.IO;
using System.Xml;

namespace QuickPay.Configurations
{
    /// <summary>配置文件转化
    /// </summary>
    public class ConfigurationFileTranslator : IConfigurationFileTranslator
    {
        private readonly IJsonSerializer _jsonSerializer;
        /// <summary>Ctor
        /// </summary>
        public ConfigurationFileTranslator(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>读取支付宝和微信配置
        /// </summary>
        public ConfigWapper TranslateToConfigWapper(string file, string format = QuickPaySettings.ConfigFormat.Json)
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
                IgnoreComments = true //忽略文档里面的注释
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
            configWapper.WechatPayConfig = new WeChatPayConfig()
            {
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
                configWapper.WechatPayConfig.Apps.Add(weChatPayApp);
            }
            return configWapper;
        }
    }
}
