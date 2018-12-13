﻿namespace QuickPay.WechatPay.Apps
{
    /// <summary>微信应用
    /// </summary>
    public class WechatPayAppOverride
    {
        /// <summary>应用名称
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

        /// <summary>Ctor
        /// </summary>
        public WechatPayAppOverride()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="appId">AppId</param>
        /// <param name="mchId">商户号</param>
        /// <param name="key">加密Key</param>
        /// <param name="appsecret">加密Secret</param>
        /// <param name="appTypeId">应用类型</param>
        /// <param name="info">移动端配置信息</param>
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

        /// <summary>WechatPayAppOverride 转 WechatPayApp
        /// </summary>
        public WechatPayApp ToWechatPayApp()
        {
            return new WechatPayApp(Name, AppId, MchId, Key, Appsecret, AppTypeId, NativeMobileInfo);
        }
    }

}
