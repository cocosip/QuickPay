using DotCommon.Extensions;
using QuickPay.Errors;
using QuickPay.Middleware;
using QuickPay.WechatPay.Apps;
using QuickPay.WechatPay.Util;
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
            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
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
                await _next.Invoke(context);
            }
        }

    }
}
