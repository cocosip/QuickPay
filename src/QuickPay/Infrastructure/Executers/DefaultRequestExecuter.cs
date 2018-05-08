using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using System.Threading.Tasks;

namespace QuickPay.Infrastructure.Executers
{
    /// <summary>执行器
    /// </summary>
    public class DefaultRequestExecuter : IRequestExecuter
    {


        private readonly IQuickPayPipelineBuilder _quickPayPipelineBuilder;
        private readonly IExecuteContextFactory _executeContextFactory;
        private readonly IQuickPayConfigManager _quickPayConfigManager;
        public DefaultRequestExecuter(IQuickPayPipelineBuilder quickPayPipelineBuilder, IExecuteContextFactory executeContextFactory, IQuickPayConfigManager quickPayConfigManager)
        {
            _quickPayPipelineBuilder = quickPayPipelineBuilder;
            _executeContextFactory = executeContextFactory;
            _quickPayConfigManager = quickPayConfigManager;
        }

        public async Task<T> ExecuteAsync<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse
        {
            var firstDelegate = _quickPayPipelineBuilder.Build();
            //当前请求的配置
            var config = _quickPayConfigManager.GetCurrentConfig(request.Provider);

            var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Execute);
            await firstDelegate(context);
            return context.Response as T;
        }


        public async Task<T> SignRequest<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse
        {
            var firstDelegate = _quickPayPipelineBuilder.Build();
            //当前请求的配置
            var config = _quickPayConfigManager.GetCurrentConfig(request.Provider);

            var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Sign);
            await firstDelegate(context);
            return context.Response as T;
        }

    }
}
