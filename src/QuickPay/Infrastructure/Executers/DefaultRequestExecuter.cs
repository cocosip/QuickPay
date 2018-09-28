using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using System;
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
        private readonly ILogger _logger;
        public DefaultRequestExecuter(ILogger<QuickPayLoggerName> logger, IQuickPayPipelineBuilder quickPayPipelineBuilder, IExecuteContextFactory executeContextFactory, IQuickPayConfigManager quickPayConfigManager)
        {
            _logger = logger;
            _quickPayPipelineBuilder = quickPayPipelineBuilder;
            _executeContextFactory = executeContextFactory;
            _quickPayConfigManager = quickPayConfigManager;
        }

        public async Task<T> ExecuteAsync<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse
        {
            try
            {
                var firstDelegate = _quickPayPipelineBuilder.Build();
                //当前请求的配置
                var config = _quickPayConfigManager.GetCurrentConfig(request.Provider);

                var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Execute);
                await firstDelegate(context);

                if (context.Response != null)
                {
                    return context.Response as T;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<T> SignRequest<T>(IPayRequest<T> request, QuickPayApp app) where T : PayResponse
        {
            try
            {
                var firstDelegate = _quickPayPipelineBuilder.Build();
                //当前请求的配置
                var config = _quickPayConfigManager.GetCurrentConfig(request.Provider);

                var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Sign);
                await firstDelegate(context);
                if (context.Response != null)
                {
                    return context.Response as T;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
