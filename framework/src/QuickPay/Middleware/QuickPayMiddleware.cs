using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace QuickPay.Middleware
{
    /// <summary>QuickPayMiddleware
    /// </summary>
    public abstract class QuickPayMiddleware
    {
        /// <summary>中间件名称
        /// </summary>
        public string MiddlewareName => this.GetType().Name;

        /// <summary>Provider
        /// </summary>
        protected IServiceProvider Provider { get; private set; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }


        /// <summary>Ctor
        /// </summary>
        public QuickPayMiddleware(IServiceProvider provider)
        {
            Provider = provider;
            Logger = provider.GetService<ILoggerFactory>().CreateLogger(QuickPaySettings.LoggerName);
        }

        /// <summary>设置错误信息
        /// </summary>
        public void SetPipelineError(ExecuteContext context, List<Error> errors)
        {
            foreach (var error in errors)
            {
                SetPipelineError(context, error);
            }
        }

        /// <summary>设置错误信息
        /// </summary>
        public void SetPipelineError(ExecuteContext context, Error error)
        {
            if (context.Request == null)
            {
                Logger.LogError($"支付执行出错,并且Context.Request为NULL.,{error.Message}");
            }
            else
            {
                Logger.LogError(context.Request?.GetLogFormat(error.Message));
            }
            context?.Errors.Add(error);
        }

    }
}
