using DotCommon.Extensions;
using DotCommon.Utility;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>自动设置请求的UniqueId
    /// </summary>
    public class AutoUniqueIdMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        /// <summary>Ctor
        /// </summary>
        public AutoUniqueIdMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            Logger = logger;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request == null)
            {
                SetPipelineError(context, new SetUniqueIdError("设置UniqueId发生错误,Context.Request为NULL"));
                return;
            }
            try
            {
                //自动设置UniqueId与Code
                if (context.Request.UniqueId.IsNullOrWhiteSpace())
                {
                    context.Request.UniqueId = ObjectId.GenerateNewStringId();
                }
                if (context.Request.BusinessCode.IsNullOrWhiteSpace())
                {
                    context.Request.BusinessCode = QuickPaySettings.DefaultBusinessCode;
                }

                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {

                    //是否为继承了BaseAlipayRequest<> 类型
                    //获取支付宝 Context.Request.BizContentRequest中的Id与BusinessCode,并且赋值到Context.Request中
                    if (context.Request.GetType() == typeof(BaseAlipayRequest<>))
                    {
                        var castRequest = (BaseAlipayRequest<BaseAlipayResponse>)context.Request;
                        if (castRequest.BizContentRequest == null)
                        {
                            SetPipelineError(context, new SetUniqueIdError("BizContentRequest为NULL"));
                            return;
                        }
                        //将Request中的UniqueId设置到Context上
                        if (!castRequest.BizContentRequest.UniqueId.IsNullOrWhiteSpace())
                        {
                            context.Request.UniqueId = castRequest.BizContentRequest.UniqueId;
                        }
                        if (!castRequest.BizContentRequest.BusinessCode.IsNullOrWhiteSpace())
                        {
                            context.Request.BusinessCode = castRequest.BizContentRequest.BusinessCode;
                        }
                    }

                }
                Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new SetUniqueIdError($"设置AutoUniqueId发生错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
