namespace QuickPay.Notify
{
    /// <summary>异步通知管理
    /// </summary>
    public interface INotifyManager
    {
        /// <summary>从Defination中注册通知
        /// </summary>
        void RegisterNotifyFromDefination(NotifyDefination notifyDefination);

        /// <summary>根据UrlFragments查询Notify
        /// </summary>
        AbstractNotify FindNotifyByUrlFragments(string urlFragments);
    }
}
