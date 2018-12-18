using DotCommon.Serializing;
using QuickPay.Alipay.Apps;
using QuickPay.WechatPay.Apps;
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

            var wechatPayNode = root.SelectSingleNode("WechatPay");
            configWapper.WechatPayConfig = new WechatPayConfig()
            {
                DefaultAppName = wechatPayNode.SelectSingleNode("DefaultAppName").InnerText,
                NotifyGateway = wechatPayNode.SelectSingleNode("NotifyGateway").InnerText,
                LocalAddress = wechatPayNode.SelectSingleNode("LocalAddress").InnerText,
                WebGateway = wechatPayNode.SelectSingleNode("WebGateway").InnerText,
                NotifyUrlFragments = wechatPayNode.SelectSingleNode("NotifyUrlFragments").InnerText,
                SignType = wechatPayNode.SelectSingleNode("SignType").InnerText,
                SslPassword = wechatPayNode.SelectSingleNode("SslPassword").InnerText,
            };

            //wechatPayConfig Apps节点
            var wechatPayAppNodes = wechatPayNode.SelectSingleNode("Apps");
            foreach (XmlNode wechatPayAppNode in wechatPayAppNodes)
            {
                var wechatPayApp = new WechatPayApp()
                {
                    AppId = wechatPayAppNode.SelectSingleNode("AppId").InnerText,
                    AppTypeId = int.Parse(wechatPayAppNode.SelectSingleNode("AppTypeId").InnerText),
                    Name = wechatPayAppNode.SelectSingleNode("Name").InnerText,
                    MchId = wechatPayAppNode.SelectSingleNode("MchId").InnerText,
                    Key = wechatPayAppNode.SelectSingleNode("Key").InnerText,
                    Appsecret = wechatPayAppNode.SelectSingleNode("Appsecret").InnerText,
                };
                var nativeMobileInfoNode = wechatPayAppNode.SelectSingleNode("NativeMobileInfo");
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
