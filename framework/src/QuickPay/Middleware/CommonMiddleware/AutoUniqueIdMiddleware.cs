using DotCommon.Extensions;
using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>自动设置请求的UniqueId
    /// </summary>
    public class AutoUniqueIdMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        /// <summary>Ctor
        /// </summary>
        public AutoUniqueIdMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next) : base(provider)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request == null)
            {
                SetPipelineError(context, new SetUniqueIdError("设置UniqueId发生错误,Context.Request为NULL"));
                return;
            }
            try
            {
                //自动设置UniqueId与Code
                if (context.Request.UniqueId.IsNullOrWhiteSpace())
                {
                    context.Request.UniqueId = ObjectId.GenerateNewStringId();
                }
                if (context.Request.BusinessCode.IsNullOrWhiteSpace())
                {
                    context.Request.BusinessCode = QuickPaySettings.DefaultBusinessCode;
                }
                Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new SetUniqueIdError($"设置AutoUniqueId发生错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
