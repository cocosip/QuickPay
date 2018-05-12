using DotCommon.Extensions;
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
        public WechatPaySignMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
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

                    if (context.SignType == WechatPaySettings.SignType.Md5)
                    {
                        var sign = WechatPayUtil.MakeSign(context.RequestPayData, (WechatPayApp)context.App);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }
                    else if (context.SignType == WechatPaySettings.SignType.Md5)
                    {
                        var sign = WechatPayUtil.Sha1Sign(context.RequestPayData);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"微信签名发生错误,{ex.Message}"));
                SetPipelineError(context, new SignError("微信签名发生错误"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
