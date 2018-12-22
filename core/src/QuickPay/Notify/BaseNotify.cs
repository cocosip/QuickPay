using QuickPay.Infrastructure.RequestData;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>通知抽象基类
    /// </summary>
    public abstract class BaseNotify : INotify
    {
        /// <summary>管道名
        /// </summary>
        public abstract string Provider { get; }

        /// <summary>通知Url片段
        /// </summary>
        public abstract string UrlFragments { get; }

        /// <summary>是否为真实通知
        /// </summary>
        public abstract Task<bool> IsRealNotify(string notifyBody);

        /// <summary>执行业务
        /// </summary>
        public abstract Task InvokeAsync(string notifyBody);

    }
}
