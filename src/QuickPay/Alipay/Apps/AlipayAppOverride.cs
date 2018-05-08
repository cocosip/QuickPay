namespace QuickPay.Alipay.Apps
{
    public class AlipayAppOverride
    {

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

        /// <summary>加密类型
        /// </summary>
        public string EncryptType { get; set; }

        /// <summary>AES加密的Key
        /// </summary>
        public string EncryptKey { get; set; }

        /// <summary>应用类型
        /// </summary>
        public int AppTypeId { get; set; }

        public AlipayAppOverride()
        {

        }

        public AlipayAppOverride(string name, string appId, string charset, string signType, string publicKey, string privateKey, int appTypeId, bool enableEncrypt, string encryptType, string encryptKey)
        {
            Name = name;
            AppId = appId;
            Charset = charset;
            SignType = signType;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            AppTypeId = appTypeId;
            EnableEncrypt = enableEncrypt;
            EncryptType = encryptType;
            EncryptKey = encryptKey;
        }

        public AlipayApp ToAlipayApp()
        {
            return new AlipayApp(Name, AppId, Charset, SignType, PublicKey, PrivateKey, AppTypeId, EnableEncrypt, EncryptType, EncryptKey);
        }
    }
}
