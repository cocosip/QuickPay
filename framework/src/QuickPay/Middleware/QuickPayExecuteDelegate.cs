using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>QuickPay管道委托
    /// </summary>
    public delegate Task QuickPayExecuteDelegate(ExecuteContext executeContext);
}
