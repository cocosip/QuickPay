using DotCommon.Extensions;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;

namespace QuickPay.Middleware
{
    /// <summary>执行上下文工厂
    /// </summary>
    public class ExecuteContextFactory : IExecuteContextFactory
    {
        /// <summary>创建执行上下文
        /// </summary>
        public ExecuteContext CreateContext<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app, string requestHandler) where T : PayResponse
        {
            var context = new ExecuteContext()
            {
                Request = request,
                Config = config,
                App = app,
                SignFieldName = request.SignFieldName,
                RequestHandler = requestHandler,
                Response = default(T)
            };

            if (!request.SignTypeName.IsNullOrWhiteSpace())
            {
                context.SignType = request.SignTypeName;
            }
            return context;
        }
    }
}
