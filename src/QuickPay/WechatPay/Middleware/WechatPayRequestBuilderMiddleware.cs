using DotCommon.Http;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using QuickPay.WechatPay.Util;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    public class WechatPayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public WechatPayRequestBuilderMiddleware(QuickPayExecuteDelegate next)
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
                        var requestXml = context.RequestPayData.ToXml();
                        var urlProperty = context.Request.GetType().GetProperties().FirstOrDefault(x => x.Name == "RequestUrl");
                        if (urlProperty == null)
                        {
                            SetPipelineError(context, new ExecuteError($"{QuickPaySettings.RequestHandler.Execute},必须要有请求url"));
                            return;
                        }

                        //发送http请求
                        context.RequestBuilder = RequestBuilder.Instance(urlProperty.GetValue(context.Request).ToString(), RequestConsts.Methods.Post)
                                .SetPost(PostType.Xml, requestXml);
                    }

                    Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"构建RequestBuilder错误,{ex.Message}"));
                SetPipelineError(context, new ExecuteError("微信构建RequestBuilder错误"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
