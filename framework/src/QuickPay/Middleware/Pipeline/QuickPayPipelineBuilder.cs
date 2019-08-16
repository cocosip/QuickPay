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

        /// <summary>Provider
        /// </summary>
        public IServiceProvider Provider { get; }
        /// <summary>Ctor
        /// </summary>
        public QuickPayPipelineBuilder(IServiceProvider provider)
        {
            Provider = provider;
            _middlewares = new List<Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate>>();
        }

        /// <summary>Use
        /// </summary>
        public IQuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware)
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
