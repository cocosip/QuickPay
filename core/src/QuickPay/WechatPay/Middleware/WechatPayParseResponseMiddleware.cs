using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using QuickPay.WechatPay.Responses;
using QuickPay.WechatPay.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    /// <summary>微信支付结果转化中间件
    /// </summary>
    public class WechatPayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        /// <summary>Ctor
        /// </summary>
        public WechatPayParseResponseMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            Logger = logger;
        }
        /// <summary>Invoke
        /// </summary>
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
                        //有可能返回的不是期望的
                        if (!payData.GetValues().Any())
                        {
                            SetPipelineError(context, new ParseResponseError("微信支付返回结果转化结果为空"));
                            return;
                        }
                        var failMsg = payData.GetMsgIfReturnFail();
                        if (failMsg != "")
                        {
                            SetPipelineError(context, new ParseResponseError($"微信支付出错,{failMsg}"));
                            return;
                        }


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
                SetPipelineError(context, new ParseResponseError($"转化执行结果发生错误,{ex.Message}"));
            }
            await _next.Invoke(context);
        }
    }
}
