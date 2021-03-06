﻿using QuickPay.Errors;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Responses;
using QuickPay.WeChatPay.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Middleware
{
    /// <summary>微信支付结果转化中间件
    /// </summary>
    public class WeChatPayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly WeChatPayDataHelper _weChatPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public WeChatPayParseResponseMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, WeChatPayDataHelper weChatPayDataHelper) : base(provider)
        {
            _next = next;
            _weChatPayDataHelper = weChatPayDataHelper;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.WeChatPay)
                {
                    var responseType = context.Request.GetType().BaseType.GetGenericArguments()[0];

                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var payData = _weChatPayDataHelper.FromXml(context.HttpResponseString);
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
                        if (typeof(WeChatPayTradeSourceResponse).IsAssignableFrom(responseType))
                        {
                            ((WeChatPayTradeSourceResponse)context.Response).PayData = new PayData(context.RequestPayData.GetValues());
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
