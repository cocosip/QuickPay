using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Requests;
using QuickPay.Alipay.Responses;
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
        /// <summary>Ctor
        /// </summary>
        public AlipayPayDataTransformMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, AlipayPayDataHelper alipayPayDataHelper)
        {
            _next = next;
            Logger = logger;
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
                    //是否为继承了BaseAlipayRequest<> 类型
                    if (context.Request.GetType() == typeof(BaseAlipayRequest<>))
                    {
                        var castRequest = (BaseAlipayRequest<BaseAlipayResponse>)context.Request;
                        if (castRequest.BizContentRequest == null)
                        {
                            SetPipelineError(context, new PayDataTransformError("BizContentRequest为NULL"));
                            return;
                        }
                        var value = castRequest.BizContentRequest;
                        var bizContent = _alipayPayDataHelper.ToJson(RequestReflectUtil.ToPayData((BaseBizContentRequest)castRequest.BizContentRequest));
                        castRequest.BizContent = bizContent;
                        //重新赋值
                        context.Request = castRequest;
                    }
                    else
                    {
                        Logger.LogInformation(context.Request.GetLogFormat($"请求Request未继承BaseAlipayRequest<>"));
                    }

                    //支付宝在转换的时候,BizContent需要自动进行转换
                    // var property = context.Request.GetType().GetProperty("BizContentRequest");
                    // var bizContentRequest = property.GetValue(context.Request);
                    // if (bizContentRequest == null)
                    // {
                    //     SetPipelineError(context, new PayDataTransformError("BizContentRequest为NULL"));
                    //     return;
                    // }
                    // //bizContent内容(string)

                    // var bizContent = _alipayPayDataHelper.ToJson(RequestReflectUtil.ToPayData((BaseBizContentRequest)bizContentRequest));
                    // var bizContentProperty = context.Request.GetType().GetProperty("BizContent");
                    // bizContentProperty.SetValue(context.Request, bizContent);
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
