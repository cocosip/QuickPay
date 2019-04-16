using DotCommon.Serializing;
using Microsoft.Extensions.Logging;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Responses;
using QuickPay.Alipay.Util;
using QuickPay.Errors;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Requests;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    /// <summary>支付宝支付返回数据格式化中间件
    /// </summary>
    public class AlipayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AlipayPayDataHelper _alipayPayDataHelper;
        /// <summary>Ctor
        /// </summary>
        public AlipayParseResponseMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger, IJsonSerializer jsonSerializer, AlipayPayDataHelper alipayPayDataHelper)
        {
            _next = next;
            Logger = logger;
            _jsonSerializer = jsonSerializer;
            _alipayPayDataHelper = alipayPayDataHelper;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {
                    var responseType = context.Request.GetType().BaseType.GetGenericArguments()[0];
                    if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                    {
                        var payData = new PayData();
                        payData = _alipayPayDataHelper.FromJson(context.HttpResponseString);
                        //没有数据
                        if (!payData.GetValues().Any())
                        {
                            SetPipelineError(context, new ParseResponseError("换结果出现问题,数据为空"));
                            return;
                        }

                        //获取签名Sign
                        var signKv = payData.GetValue(context.SignFieldName);
                        //数据
                        var responseWapper = payData.GetValues().FirstOrDefault(x => x.Key != context.SignFieldName);

                        var app = (AlipayApp)context.App;
                        var sourceJson = "";
                        if (app.EnableEncrypt)
                        {
                            sourceJson = AlipayUtil.AesDecrypt(app.EncryptKey, responseWapper.ToString(), app.Charset);
                        }
                        else
                        {
                            //未使用加密
                            sourceJson = _jsonSerializer.Serialize(responseWapper);
                        }
                        payData = _alipayPayDataHelper.FromJson(sourceJson);
                        payData.SetValue(context.SignFieldName, signKv);
                        //将PayData转换为对象
                        context.Response = (PayResponse)RequestReflectUtil.ToResponse(payData, responseType);
                        //ResponsPayData
                        context.ResponsePayData = new PayData(payData.GetValues());
                    }
                    else
                    {
                        //如果是签名的请求,那么直接设置Response
                        //将PayData转换为对象
                        context.Response = (PayResponse)RequestReflectUtil.ToResponse(context.RequestPayData, responseType);
                        //ResponsPayData
                        context.ResponsePayData = new PayData(context.RequestPayData.GetValues());
                        //判断Response对象是包含PayData数据的
                        if (typeof(AlipayTradeSourceResponse).IsAssignableFrom(responseType))
                        {
                            ((AlipayTradeSourceResponse)context.Response).PayData = new PayData(context.RequestPayData.GetValues());
                        }
                    }

                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));

                }
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new ParseResponseError($"转换返回结果PayData错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }
    }
}
