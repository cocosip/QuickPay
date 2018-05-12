using QuickPay.Errors;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using QuickPay.WechatPay.Util;
using System;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    public class WechatPayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public WechatPayParseResponseMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
                {
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var payData = context.RequestPayData.FromXml(context.HttpResponseString);
                        context.ResponsePayData = new PayData(payData.GetValues());

                        var responseType = context.Request.GetType().BaseType.GetGenericArguments()[0];
                        context.Response = (PayResponse)(RequestReflectUtil.ToResponse(payData, responseType));
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"转化执行结果发生错误,{ex.Message}"));
                SetPipelineError(context, new ParseResponseError("转化执行结果发生错误"));
                return;
            }
            await _next.Invoke(context);
        }
    }
}
