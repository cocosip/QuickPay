using System.Threading.Tasks;

namespace QuickPay.Middleware
{

    public delegate Task QuickPayExecuteDelegate(ExecuteContext executeContext);
}
