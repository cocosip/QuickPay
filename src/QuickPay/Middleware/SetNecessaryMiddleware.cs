using QuickPay.Infrastructure.Apps;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class SetNecessaryMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        public SetNecessaryMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(ExecuteContext context)
        {
            var methods = context.Request.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var invokeMethods = methods.Where(m => string.Equals(m.Name, "SetNecessary", StringComparison.Ordinal)).ToArray();
            var methodinfo = invokeMethods[0];

            var setNecessaryMethod = (Action<QuickPayConfig, QuickPayApp>)methodinfo.CreateDelegate(typeof(Action<QuickPayConfig, QuickPayApp>), context.Request);
            setNecessaryMethod(context.Config, context.App);
            await _next.Invoke(context);
        }

    }
}
