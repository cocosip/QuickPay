using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Collections.Generic;

namespace QuickPay.Middleware
{
    /// <summary>QuickPayMiddleware
    /// </summary>
    public abstract class QuickPayMiddleware
    {
        /// <summary>中间件名称
        /// </summary>
        public string MiddlewareName => GetType().Name;

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
            Logger = provider.GetService<ILogger<QuickPayMiddleware>>(); ;
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
                Logger.LogError("支付执行出错,并且Context.Request为NULL,{0}", error.Message);
            }
            else
            {
                Logger.LogError(context.Request?.GetLogFormat(error.Message));
            }
            context?.Errors.Add(error);
        }

    }
}
