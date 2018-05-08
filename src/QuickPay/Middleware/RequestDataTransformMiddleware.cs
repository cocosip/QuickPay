using QuickPay.Errors;
using QuickPay.Infrastructure.Util;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>将请求PayRequest转换为PayData数据
    /// </summary>
    public class RequestDataTransformMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public RequestDataTransformMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request == null)
            {
                SetPipelineError(context, new PayDataTransformError("PayRequest请求为null"));
                return;
            }
            context.RequestPayData = RequestReflectUtil.ToPayData(context.Request);
            await _next.Invoke(context);
        }
    }
}
