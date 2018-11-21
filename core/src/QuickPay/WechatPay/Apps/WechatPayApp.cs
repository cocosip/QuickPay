using QuickPay.Infrastructure.Apps;

namespace QuickPay.WechatPay.Apps
{
    /// <summary>微信支付应用
    /// </summary>
    public class WechatPayApp : QuickPayApp
    {
        public override string Provider => QuickPaySettings.Provider.WechatPay;

        /// <summary>应用的名称
        /// </summary>
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



        public WechatPayApp()
        {

        }
        public WechatPayApp(string name, string appId, string mchId, string key, string appsecret, int appTypeId, NativeMobileInfo info)
        {
            Name = name;
            AppId = appId;
            MchId = mchId;
            Key = key;
            Appsecret = appsecret;
            AppTypeId = appTypeId;
            NativeMobileInfo = info.SelfCopy();
        }

        public WechatPayAppOverride ToOverrideValue()
        {
            return new WechatPayAppOverride(Name, AppId, MchId, Key, Appsecret, AppTypeId, NativeMobileInfo);
        }

    }
    /// <summary>移动端配置
    /// </summary>
    public class NativeMobileInfo
    {
        /// <summary>Ios应用名
        /// </summary>
        public string IosName { get; set; }

        /// <summary>Ios的BundleId
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>安卓名称
        /// </summary>
        public string AndroidName { get; set; }

        /// <summary>安卓包名
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>移动端名称
        /// </summary>
        public string WapName { get; set; }

        /// <summary>移动端Url
        /// </summary>
        public string WapUrl { get; set; }

        public NativeMobileInfo SelfCopy()
        {
            return new NativeMobileInfo()
            {
                IosName = IosName,
                BundleId = BundleId,
                AndroidName = AndroidName,
                PackageName = PackageName,
                WapName = WapName,
                WapUrl = WapUrl
            };
        }
    }
}
