using QuickPay.Infrastructure.Apps;

namespace QuickPay.Alipay.Apps
{
    /// <summary>支付宝App应用
    /// </summary>
    public class AlipayApp : QuickPayApp
    {
        /// <summary>支付宝App应用
        /// </summary>
        public override string Provider => QuickPaySettings.Provider.Alipay;

        /// <summary>应用的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>应用Id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>>请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        public string Charset { get; set; }

        /// <summary>签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>私钥
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>是否开启AES加密
        /// </summary>
        public bool EnableEncrypt { get; set; }

        /// <summary>AES加密的Key
        /// </summary>
        public string EncryptKey { get; set; }

        /// <summary>加密方式
        /// </summary>
        public string EncryptType { get; set; }

        /// <summary>应用类型
        /// </summary>
        public int AppTypeId { get; set; }

        /// <summary>支付宝应用
        /// </summary>
        public AlipayApp()
        {

        }

        /// <summary>支付宝应用
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="appId">AppId</param>
        /// <param name="charset">编码(如:utf-8)</param>
        /// <param name="signType">签名类型(如:RSA2)</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="appTypeId">应用类型</param>
        /// <param name="enableEncrypt">启用数据加密</param>
        /// <param name="encryptType">数据加密类型</param>
        /// <param name="encryptKey">数据加密Key</param>
        public AlipayApp(string name, string appId, string charset, string signType, string publicKey, string privateKey, int appTypeId, bool enableEncrypt, string encryptType, string encryptKey)
        {
            Name = name;
            AppId = appId;
            Charset = charset;
            SignType = signType;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            EnableEncrypt = enableEncrypt;
            EncryptType = encryptType;
            EncryptKey = encryptKey;
            AppTypeId = appTypeId;
        }

        /// <summary>将AlipayApp转换成AlipayAppOverride
        /// </summary>
        public AlipayAppOverride ToOverrideValue()
        {
            return new AlipayAppOverride(Name, AppId, Charset, SignType, PublicKey, PrivateKey, AppTypeId, EnableEncrypt, EncryptType, EncryptKey);
        }
    }
}
