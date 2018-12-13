using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>结尾管道
    /// </summary>
    public class EndMiddleware: QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        /// <summary>Ctor
        /// </summary>
        public EndMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            await Task.CompletedTask;
            return;
        }
    }
}
