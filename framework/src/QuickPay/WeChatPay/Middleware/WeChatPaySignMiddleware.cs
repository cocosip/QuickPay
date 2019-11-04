using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WeChatPay.Apps;
using QuickPay.WeChatPay.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.WeChatPay.Middleware
{
    /// <summary>微信支付签名中间件
    /// </summary>
    public class WeChatPaySignMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        private readonly WeChatPayDataHelper _weChatPayDataHelper;

        /// <summary>Ctor
        /// </summary>
        public WeChatPaySignMiddleware(IServiceProvider provider, QuickPayExecuteDelegate next, WeChatPayDataHelper weChatPayDataHelper) : base(provider)
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
                    if (!context.RequestPayData.GetValues().Any())
                    {
                        SetPipelineError(context, new SignError("微信支付签名PayData为空."));
                        return;
                    }
                    if (context.SignFieldName.IsNullOrEmpty())
                    {
                        SetPipelineError(context, new SignError("微信SignField字段为空."));
                        return;
                    }

                    var sign = "";

                    if (context.SignType == WeChatPaySettings.SignType.Md5)
                    {
                        sign = WeChatPayUtil.Md5Sign(context.RequestPayData, (WeChatPayApp)context.App);
                        //sign = WeChatPayUtil.MakeSign(context.RequestPayData, (WeChatPayApp)context.App);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }
                    else if (context.SignType == WeChatPaySettings.SignType.Sha1)
                    {
                        sign = WeChatPayUtil.Sha1Sign(context.RequestPayData);
                        context.RequestPayData.SetValue(context.SignFieldName, sign);
                    }

                    Logger.LogInformation(context.Request.GetLogFormat($"签名字段:{context.SignFieldName},签名:{sign},签名后数据:[{ _weChatPayDataHelper.ToXml(context.RequestPayData)}]"));
                    Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new SignError($"微信签名发生错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
