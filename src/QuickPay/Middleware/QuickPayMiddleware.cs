using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using QuickPay.Errors;
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
            Logger.LogError($"Provider:{context.Request?.Provider},{error.Message}");
            context.Errors.Add(error);
        }

    }
}
