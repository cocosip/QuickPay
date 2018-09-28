using System;

namespace QuickPay.Middleware.Pipeline
{
    public interface IQuickPayPipelineBuilder
    {
        IServiceProvider Provider { get; }

        QuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware);

        QuickPayExecuteDelegate Build();
    }
}
