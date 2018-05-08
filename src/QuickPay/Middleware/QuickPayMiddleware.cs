using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Serializing;
using QuickPay.Errors;
using System.Collections.Generic;

namespace QuickPay.Middleware
{
    public abstract class QuickPayMiddleware
    {
        public string MiddlewareName => this.GetType().Name;
        protected ILogger Logger { get; }
        protected IJsonSerializer JsonSerializer { get; }
        public QuickPayMiddleware()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
            JsonSerializer = IocManager.GetContainer().Resolve<IJsonSerializer>();
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
            Logger.Error($"Provider:{context.Request?.Provider},{error.Message}");
            context.Errors.Add(error);
        }

    }
}
