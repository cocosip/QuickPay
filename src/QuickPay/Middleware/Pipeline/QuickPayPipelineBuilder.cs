using DotCommon.Dependency;
using DotCommon.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Middleware.Pipeline
{
    public class QuickPayPipelineBuilder : IQuickPayPipelineBuilder
    {
        private readonly IList<Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate>> _middlewares;
        private readonly ILogger _logger;
        public QuickPayPipelineBuilder()
        {
            _middlewares = new List<Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate>>();
            _logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(QuickPaySettings.LoggerName);
        }


        public QuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public QuickPayExecuteDelegate Build()
        {
            QuickPayExecuteDelegate app = context =>
            {
                context.Response = null;
                return Task.CompletedTask;
            };
            foreach (var component in _middlewares.Reverse())
            {
                app = component(app);
            }
            return app;
        }
    }
}
