using DotCommon.DependencyInjection;
using QuickPay.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickPay.Notify
{
    /// <summary>通知管理
    /// </summary>
    public class NotifyManager : INotifyManager
    {
        private readonly IServiceProvider _provider;
        private readonly INotifyTypeFinder _notifyTypeFinder;
        private readonly List<INotify> _notifies;

        /// <summary>Ctor
        /// </summary>
        public NotifyManager(IServiceProvider provider, INotifyTypeFinder notifyTypeFinder)
        {
            _provider = provider;
            _notifyTypeFinder = notifyTypeFinder;
            _notifies = new List<INotify>();
        }

        /// <summary>根据UrlFragments获取通知
        /// </summary>
        public INotify FindNotifyByUrlFragments(string urlFragments)
        {
            var notify = _notifies.FirstOrDefault(x => IsUrlFragmentsMatch(x, urlFragments));
            return notify;
        }


        /// <summary>根据通知定义,添加通知
        /// </summary>
        public void AddNotifyByDefination(NotifyDefination defination)
        {
            //是否为通知类型
            if (!_notifyTypeFinder.IsNotifyType(defination.NotifyType))
            {
                throw new QuickPayException($"类型:{defination.NotifyType} 不是有效的通知类型。");
            }

            //是否为有效的
            var castNotify = (INotify)_provider.CreateInstance(defination.NotifyType);
            AddNotify(castNotify);
        }

        /// <summary>添加通知
        /// </summary>
        public void AddNotify(INotify notify)
        {
            //遍历,校验是否有重复
            foreach (var queryNotify in _notifies)
            {
                if (IsUrlFragmentsMatch(queryNotify, queryNotify.UrlFragments))
                {
                    throw new QuickPayException($"Notify:{notify.GetType()},UrlFragments:{notify.UrlFragments} 与现有的通知UrlFragments重复,OriginalNotify:{queryNotify.GetType()},OriginalUrlFragments:{queryNotify.UrlFragments}");
                }
            }
            _notifies.Add(notify);
        }

        #region Utility

        /// <summary>通知与UrlFragments匹配
        /// </summary>
        private bool IsUrlFragmentsMatch(INotify notify, string urlFragments)
        {
            if (!urlFragments.StartsWith("/"))
            {
                //增加头部 /
                urlFragments = "/" + urlFragments;
            }

            return string.Equals(notify.UrlFragments, urlFragments, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
