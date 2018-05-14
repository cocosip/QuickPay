﻿using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    /// <summary>PayRequest转换为PayData的数据
    /// </summary>
    public class WechatPayDataTransformMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public WechatPayDataTransformMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
            {
                if (context.Request == null)
                {
                    SetPipelineError(context, new PayDataTransformError("PayRequest请求为null"));
                    return;
                }
                try
                {
                    context.RequestPayData = RequestReflectUtil.ToPayData(context.Request);

        
                    Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
                catch (Exception ex)
                {
                    Logger.Error(context.Request.GetLogFormat($"转换PayData发生错误,{ex.Message}"));
                    SetPipelineError(context, new PayDataTransformError("转换PayData发生错误"));
                    return;
                }
            }
            await _next.Invoke(context);
        }

    }
}