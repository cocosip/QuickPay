using QuickPay.Middleware;
using System.Threading.Tasks;

namespace QuickPay.UnitTest.Middleware.Pipeline
{
    public class Test1Middleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        public Test1Middleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {

            await _next.Invoke(context);
        }
    }
}
