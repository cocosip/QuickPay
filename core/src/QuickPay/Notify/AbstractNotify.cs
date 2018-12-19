using QuickPay.Infrastructure.RequestData;
using System;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>抽象的通知类
    /// </summary>
    public abstract class AbstractNotify
    {

        /// <summary>ServiceProvider
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>使用的管道名
        /// </summary>
        public abstract string Provider { get; }

        /// <summary>异步通知UrlFragments
        /// </summary>
        public abstract string NotifyUrlFragments { get; }

        /// <summary>校验是否为真实的异步通知,主要校验签名
        /// </summary>
        public abstract Task<bool> IsRealNotify(string notifyBody);

        /// <summary>获取AppId
        /// </summary>
        public virtual string GetAppId(PayData payData)
        {
            return payData.GetAppId();
        }

        /// <summary>Ctor
        /// </summary>
        public AbstractNotify(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
