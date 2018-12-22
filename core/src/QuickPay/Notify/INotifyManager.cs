namespace QuickPay.Notify
{
    /// <summary>通知管理
    /// </summary>
    public interface INotifyManager
    {

        /// <summary>根据UrlFragments获取通知
        /// </summary>
        INotify FindNotifyByUrlFragments(string urlFragments);

        /// <summary>根据通知定义,添加通知
        /// </summary>
        void AddNotifyByDefination(NotifyDefination defination);

        /// <summary>添加通知
        /// </summary>
        void AddNotify(INotify notify);
    }
}
