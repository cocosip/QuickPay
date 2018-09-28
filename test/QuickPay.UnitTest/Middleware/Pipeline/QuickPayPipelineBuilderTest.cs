using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuickPay.UnitTest.Middleware.Pipeline
{
    public class QuickPayPipelineBuilderTest : TestBase
    {
 
        //[Fact]
        //public void PipelineBuilderUseMiddlewareTest()
        //{
        //    var pipelineBuilder = IocManager.GetContainer().Resolve<IQuickPayPipelineBuilder>();
        //    QuickPayExecuteDelegate quickPayExecuteDelegate = ctx =>
        //    {
        //        Console.Write(1);
        //        return Task.CompletedTask;
        //    };
        //    pipelineBuilder.UseMiddleware<ErrorHandlerMiddleware>(quickPayExecuteDelegate);

        //    var f = pipelineBuilder.Build();
        //}
    }
}
