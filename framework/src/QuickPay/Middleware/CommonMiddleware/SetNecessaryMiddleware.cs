using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>设置必要参数中间件
    /// </summary>
    public class SetNecessaryMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        /// <summary>Ctor
        /// </summary>
        public SetNecessaryMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next) : base(provider)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                //执行SetNecessary
                context.Request.SetNecessary(context.Config, context.App);
                //重新赋值
                if (context.SignType.IsNullOrWhiteSpace())
                {
                    context.SignType = context.Request.SignTypeName;
                }

                Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new SetNecessaryError($"设置Necessary发生错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
