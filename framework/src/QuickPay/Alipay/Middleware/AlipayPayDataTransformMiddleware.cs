﻿using AspectCore.Extensions.Reflection;
using DotCommon.Reflecting;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Utility;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    /// <summary>PayRequest转换为PayData的数据
    /// </summary>
    public class AlipayPayDataTransformMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly AlipayPayDataHelper _alipayPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public AlipayPayDataTransformMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, AlipayPayDataHelper alipayPayDataHelper) : base(provider)
        {
            _next = next;
            _alipayPayDataHelper = alipayPayDataHelper;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {

            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                if (context.Request == null)
                {
                    SetPipelineError(context, new PayDataTransformError("PayRequest请求为NULL"));
                    return;
                }
                try
                {

                    //支付宝在转换的时候,BizContent需要自动进行转换
                    if (ReflectionUtil.IsAssignableToGenericType(context.Request.GetType(), typeof(BaseAlipayRequest<>)))
                    {
                        var bizContentRequestProperty = context.Request.GetType().GetProperty("BizContentRequest");
                        var bizContentRequestPropertyReflector = bizContentRequestProperty.GetReflector();
                        var bizContentRequest = bizContentRequestPropertyReflector.GetValue(context.Request);
                        if (bizContentRequest == null)
                        {
                            SetPipelineError(context, new PayDataTransformError("BizContentRequest为NULL"));
                            return;
                        }
                        //bizContent内容(string)

                        var bizContent = _alipayPayDataHelper.ToJson(RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest));
                        var bizContentProperty = context.Request.GetType().GetProperty("BizContent");
                        var bizContentPropertyReflector = bizContentProperty.GetReflector();
                        bizContentPropertyReflector.SetValue(context.Request, bizContent);
                    }
                    //将Request转换为PayData
                    context.RequestPayData = RequestReflectUtil.ToPayData(context.Request);

                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
                catch (Exception ex)
                {
                    SetPipelineError(context, new PayDataTransformError($"转换PayData发生错误,{ex.Message}"));
                    return;
                }
            }
            await _next.Invoke(context);
        }

    }
}
