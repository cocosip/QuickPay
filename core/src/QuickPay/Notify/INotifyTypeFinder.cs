using System;

namespace QuickPay.Notify
{
    /// <summary>通知类型管理
    /// </summary>
    public interface INotifyTypeFinder
    {
        /// <summary>是否为通知类型
        /// </summary>
        bool IsNotifyType(Type type);

        /// <summary>获取通知类型的UrlFragments
        /// </summary>
        string FindUrlFragments(Type type);
    }
}
