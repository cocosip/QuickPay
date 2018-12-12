using DotCommon.Extensions;
using QuickPay.Alipay.Apps;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.WechatPay.Apps;

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
