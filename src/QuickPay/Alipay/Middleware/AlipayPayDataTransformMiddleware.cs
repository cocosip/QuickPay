﻿using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Util;
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
        public AlipayPayDataTransformMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, AlipayPayDataHelper alipayPayDataHelper)
        {
            _next = next;
            Logger = logger;
            _alipayPayDataHelper = alipayPayDataHelper;
        }

        public async Task Invoke(ExecuteContext context)
        {

            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {

                if (context.Request == null)
                {
                    SetPipelineError(context, new PayDataTransformError("PayRequest请求为null"));
                    return;
                }
                try
                {
                    //支付宝在转换的时候,BizContent需要自动进行转换
                    var property = context.Request.GetType().GetProperty("BizContentRequest");
                    var bizContentRequest = property.GetValue(context.Request);
                    if (bizContentRequest == null)
                    {
                        SetPipelineError(context, new PayDataTransformError("BizContentRequest为null"));
                        return;
                    }
                    //bizContent内容(string)

                    var bizContent = _alipayPayDataHelper.ToJson(RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest));
                    var bizContentProperty = context.Request.GetType().GetProperty("BizContent");
                    bizContentProperty.SetValue(context.Request, bizContent);
                    //将Request转换为PayData
                    context.RequestPayData = RequestReflectUtil.ToPayData(context.Request);

                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
                catch (Exception ex)
                {
                    Logger.LogError(context.Request.GetLogFormat($"转换PayData发生错误,{ex.Message}"));
                    SetPipelineError(context, new PayDataTransformError("转换PayData发生错误"));
                    return;
                }
            }
            await _next.Invoke(context);
        }

    }
}
