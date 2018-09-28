using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    public class WechatPayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        public WechatPayParseResponseMiddleware(QuickPayExecuteDelegate next,ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            Logger = logger;
        }
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
                {
                    var responseType = context.Request.GetType().BaseType.GetGenericArguments()[0];

                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var payData = context.RequestPayData.FromXml(context.HttpResponseString);
                        context.ResponsePayData = new PayData(payData.GetValues());

                        context.Response = (PayResponse)(RequestReflectUtil.ToResponse(payData, responseType));
                    }
                    else
                    {
                        //如果是签名的请求,那么直接设置Response
                        //将PayData转换为对象
                        context.Response = (PayResponse)RequestReflectUtil.ToResponse(context.RequestPayData, responseType);
                        //ResponsPayData
                        context.ResponsePayData = new PayData(context.RequestPayData.GetValues());

                        //判断Response对象是包含PayData数据的
                        if (typeof(WechatPayTradeSourceResponse).IsAssignableFrom(responseType))
                        {
                            ((WechatPayTradeSourceResponse)context.Response).PayData = new PayData(context.RequestPayData.GetValues());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(context.Request.GetLogFormat($"转化执行结果发生错误,{ex.Message}"));
                SetPipelineError(context, new ParseResponseError("转化执行结果发生错误"));
                return;
            }
            await _next.Invoke(context);
        }
    }
}
