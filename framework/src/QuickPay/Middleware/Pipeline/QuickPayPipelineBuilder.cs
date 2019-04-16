using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Middleware.Pipeline
{
    /// <summary>QuickPay管道创建
    /// </summary>
    public class QuickPayPipelineBuilder : IQuickPayPipelineBuilder
    {
        private readonly IList<Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate>> _middlewares;
        private readonly ILogger _logger;

        /// <summary>Provider
        /// </summary>
        public IServiceProvider Provider { get; }
        /// <summary>Ctor
        /// </summary>
        public QuickPayPipelineBuilder(ILogger<QuickPayLoggerName> logger, IServiceProvider provider)
        {
            _logger = logger;
            Provider = provider;
            _middlewares = new List<Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate>>();
        }

        /// <summary>Use
        /// </summary>
        public QuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        /// <summary>Build
        /// </summary>
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
