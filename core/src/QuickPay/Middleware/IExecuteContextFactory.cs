using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Middleware
{
    /// <summary>执行上下文工厂
    /// </summary>
    public interface IExecuteContextFactory
    {
        /// <summary>创建执行上下文
        /// </summary>
        ExecuteContext CreateContext<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app, string requestHandler) where T : PayResponse;
    }
}
