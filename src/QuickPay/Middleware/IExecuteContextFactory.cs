using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Middleware
{
    public interface IExecuteContextFactory
    {
        ExecuteContext CreateContext<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app, string requestHandler) where T : PayResponse;
    }
}
