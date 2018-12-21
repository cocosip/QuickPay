using QuickPay.Infrastructure.RequestData;
using System.Threading.Tasks;

namespace QuickPay.Notify
{
    /// <summary>通知策略
    /// </summary>
    public interface INotifyPolicy
    {

        /// <summary>管道名
        /// </summary>
        string Provider { get; }

        /// <summary>是否为真实的通知(通知签名校验)
        /// </summary>
        Task<bool> IsRealNotify(PayData payData);
    }
}
