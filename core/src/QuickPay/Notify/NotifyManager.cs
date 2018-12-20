using DotCommon.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Notify
{
    /// <summary>异步通知管理
    /// </summary>
    public class NotifyManager : INotifyManager
    {
        /// <summary>Notifies
        /// </summary>
        private List<AbstractNotify> Notifies { get; }

        /// <summary>Provider
        /// </summary>
        private IServiceProvider Provider { get; }

        /// <summary>Logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>Ctor
        /// </summary>
        public NotifyManager(IServiceProvider provider, ILogger<QuickPayLoggerName> logger)
        {
            Provider = provider;
            _logger = logger;
            Notifies = new List<AbstractNotify>();
        }

        /// <summary>从Defination中注册通知
        /// </summary>
        public void RegisterNotifyFromDefination(NotifyDefination notifyDefination)
        {
            //判断是否已经创建了该Notify的具体实例
            if (Notifies.Any(x => x.GetType() == notifyDefination.NotifyType))
            {
                //创建Notify对象
                var notify = (AbstractNotify)Provider.CreateInstance(notifyDefination.NotifyType);
                Notifies.Add(notify);
            }
        }

        /// <summary>根据UrlFragments查询Notify
        /// </summary>
        public AbstractNotify FindNotifyByUrlFragments(string urlFragments)
        {
            var notifies = Notifies.Where(x => IsUrlFragmentsMatch(x, urlFragments)).ToList();
            if (notifies.Count() > 1)
            {
                var notifyTypeNames = notifies.Select(x => x.GetType().ToString());
                _logger.LogError("UrlFragments匹配到多个Notify。UrlFragments:{0},Notifies:[{1}]", urlFragments, string.Join(",", notifyTypeNames));
            }
            return notifies.FirstOrDefault();
        }

        #region Utils

        /// <summary>Notify与UrlFragments进行匹配
        /// </summary>
        private bool IsUrlFragmentsMatch(AbstractNotify notify, string urlFragments)
        {
            //去除'/'结尾
            urlFragments = urlFragments.TrimEnd('/');
            if (!urlFragments.StartsWith("/"))
            {
                urlFragments = "/" + urlFragments;
            }

            if (notify.NotifyUrlFragments.Equals(urlFragments, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
