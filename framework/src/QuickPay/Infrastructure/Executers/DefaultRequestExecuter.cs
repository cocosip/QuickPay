﻿using Microsoft.Extensions.Logging;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Middleware;
using QuickPay.Middleware.Pipeline;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Infrastructure.Executers
{
    /// <summary>执行器
    /// </summary>
    public class DefaultRequestExecuter : IRequestExecuter
    {
        private readonly IQuickPayPipelineBuilder _quickPayPipelineBuilder;
        private readonly IExecuteContextFactory _executeContextFactory;

        private readonly ILogger _logger;
        /// <summary>
        /// </summary>
        public DefaultRequestExecuter(ILogger<DefaultRequestExecuter> logger, IQuickPayPipelineBuilder quickPayPipelineBuilder, IExecuteContextFactory executeContextFactory)
        {
            _logger = logger;
            _quickPayPipelineBuilder = quickPayPipelineBuilder;
            _executeContextFactory = executeContextFactory;
        }

        /// <summary>执行器执行
        /// </summary>
        public async Task<T> ExecuteAsync<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app) where T : PayResponse
        {
            try
            {
                var firstDelegate = _quickPayPipelineBuilder.Build();
                var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Execute);
                await firstDelegate(context);
                if (context.IsError)
                {
                    var error = context.Errors.FirstOrDefault();
                    throw new Exception(error.Message);
                }

                if (context.Response != null)
                {
                    return context.Response as T;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"支付ExecuteAsync出错,{ex.Message}");
                throw ex;
            }
        }

        /// <summary>请求签名
        /// </summary>
        public async Task<T> SignRequest<T>(IPayRequest<T> request, QuickPayConfig config, QuickPayApp app) where T : PayResponse
        {
            try
            {
                var firstDelegate = _quickPayPipelineBuilder.Build();
                var context = _executeContextFactory.CreateContext<T>(request, config, app, QuickPaySettings.RequestHandler.Sign);
                await firstDelegate(context);
                if (context.IsError)
                {
                    var error = context.Errors.FirstOrDefault();
                    throw new Exception(error.Message);
                }

                if (context.Response != null)
                {
                    return context.Response as T;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"支付SignRequest出错,{ex.Message}");
                throw ex;
            }
        }

    }
}
