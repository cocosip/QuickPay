using System;

namespace QuickPay.Middleware.Pipeline
{
    public interface IQuickPayPipelineBuilder
    {
        QuickPayPipelineBuilder Use(Func<QuickPayExecuteDelegate, QuickPayExecuteDelegate> middleware);

        QuickPayExecuteDelegate Build();
    }
}
