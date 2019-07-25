using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Middleware
{
    /// <summary>PayRequest转换为PayData的数据
    /// </summary>
    public class WeChatPayDataTransformMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        /// <summary>Ctor
        /// </summary>
        public WeChatPayDataTransformMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next) : base(provider)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.WeChatPay)
            {
                if (context.Request == null)
                {
                    SetPipelineError(context, new PayDataTransformError("PayRequest请求为NULL"));
                    return;
                }
                try
                {
                    context.RequestPayData = RequestReflectUtil.ToPayData(context.Request);
                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
                catch (Exception ex)
                {
                    SetPipelineError(context, new PayDataTransformError($"转换PayData发生错误,{ex.Message}"));
                    return;
                }
            }
            await _next.Invoke(context);
        }

    }
}
