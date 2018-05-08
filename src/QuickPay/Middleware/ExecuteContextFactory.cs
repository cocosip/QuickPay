using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Middleware
{
    public class ExecuteContextFactory : IExecuteContextFactory
    {
        public ExecuteContext CreateContext<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app, string requestHandler) where T : PayResponse
        {
            var context = new ExecuteContext()
            {
                Request = request,
                Config = config,
                App = app,
                SignFieldName = "",
                SignType = "",
                RequestHandler = requestHandler,
                Response = default(T)
            };

            return context;
        }
    }
}
