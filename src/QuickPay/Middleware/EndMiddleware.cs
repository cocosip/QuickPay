using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class EndMiddleware: QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public EndMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            await Task.CompletedTask;
            return;
        }
    }
}
