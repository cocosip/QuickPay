using DotCommon.DependencyInjection;
using System;

namespace QuickPay.Notify
{
    /// <summary>通知类型管理
    /// </summary>
    public class NotifyTypeFinder : INotifyTypeFinder
    {
        private readonly IServiceProvider _provider;

        /// <summary>Ctor
        /// </summary>
        public NotifyTypeFinder(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>是否为通知类型
        /// </summary>
        public bool IsNotifyType(Type type)
        {
            return type.IsAssignableFrom(typeof(INotify)) && !type.IsAbstract;
        }

        /// <summary>获取通知类型的UrlFragments
        /// </summary>
        public string FindUrlFragments(Type type)
        {
            var castNotify = (INotify)_provider.CreateInstance(type);
            return castNotify.UrlFragments;
        }

    }
}
