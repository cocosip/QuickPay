using System;

namespace QuickPay.Middleware.Pipeline
{
    /// <summary>QuickPay管道创建
    /// </summary>
    public interface IQuickPayPipelineBuilder
    {
        /// <summary>Provider
        /// </summary>
        IServiceProvider Provider { get; }

        /// <summary>Use
        /// </summary>
        IQuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware);
        
        /// <summary>Build
        /// </summary>
        QuickPayExecuteDelegate Build();
    }
}
