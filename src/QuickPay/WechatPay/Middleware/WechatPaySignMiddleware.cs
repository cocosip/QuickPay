using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    public class WechatPaySignMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public WechatPaySignMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger)
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
                    if (!context.RequestPayData.GetValues().Any())
                    {
                        SetPipelineError(context, new SignError("微信支付签名PayData为空."));
                        return;
                    }
                    if (context.SignFieldName.IsNullOrEmpty())
                    {
                        SetPipelineError(context, new SignError("SignField字段为空."));
                        return;
                    }

                    var sign = "";

                    if (context.SignType == WechatPaySettings.SignType.Md5)
                    {
                        sign = WechatPayUtil.MakeSign(context.RequestPayData, (WechatPayApp)context.App);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }
                    else if (context.SignType == WechatPaySettings.SignType.Md5)
                    {
                        sign = WechatPayUtil.Sha1Sign(context.RequestPayData);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }

                    Logger.LogInformation(context.Request.GetLogFormat($"签名字段:{context.SignFieldName},签名:{sign},签名后数据:[{ context.RequestPayData.ToXml()}]"));
                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(context.Request.GetLogFormat($"微信签名发生错误,{ex.Message}"));
                SetPipelineError(context, new SignError("微信签名发生错误"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
