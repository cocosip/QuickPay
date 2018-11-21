namespace QuickPay.WechatPay.Apps
{
    public class WechatPayAppOverride
    {
        public string Name { get; set; }
        /// <summary>应用Id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>加密的Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>密码
        /// </summary>
        public string Appsecret { get; set; }

        /// <summary>应用类型,1-公众号,2-应用
        /// </summary>
        public int AppTypeId { get; set; }

        /// <summary>移动端信息
        /// </summary>
        public NativeMobileInfo NativeMobileInfo { get; set; }

        public WechatPayAppOverride()
        {

        }

        public WechatPayAppOverride(string name, string appId, string mchId, string key, string appsecret, int appTypeId, NativeMobileInfo info)
        {
            Name = name;
            AppId = appId;
            MchId = mchId;
            Key = key;
            Appsecret = appsecret;
            AppTypeId = appTypeId;
            NativeMobileInfo = info.SelfCopy();
        }

        public WechatPayApp ToWechatPayApp()
        {
            return new WechatPayApp(Name, AppId, MchId, Key, Appsecret, AppTypeId, NativeMobileInfo);
        }
    }

}
