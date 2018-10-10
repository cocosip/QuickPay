using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System.Collections.Generic;
namespace QuickPay.Middleware
{
    public abstract class QuickPayMiddleware
    {
        public string MiddlewareName => this.GetType().Name;
        protected ILogger Logger { get; set; }
        public QuickPayMiddleware()
        {
            Logger = NullLogger.Instance;
        }

        public void SetPipelineError(ExecuteContext context, List<Error> errors)
        {
            foreach (var error in errors)
            {
                SetPipelineError(context, error);
            }
        }

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
