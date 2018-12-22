using QuickPay.Infrastructure.RequestData;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>通知策略
    /// </summary>
    public interface INotify
    {

        /// <summary>管道名
        /// </summary>
        string Provider { get; }

        /// <summary>通知Url片段
        /// </summary>
        string UrlFragments { get; }

        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        Task<bool> IsRealNotify(string notifyBody);

        /// <summary>执行业务
        /// </summary>
        Task InvokeAsync(string notifyBody);
    }
}
