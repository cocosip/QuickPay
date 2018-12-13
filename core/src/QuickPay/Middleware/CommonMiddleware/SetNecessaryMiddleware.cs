using DotCommon.Extensions;
using Microsoft.Extensions.Logging;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>设置必要参数中间件
    /// </summary>
    public class SetNecessaryMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        /// <summary>Ctor
        /// </summary>
        public SetNecessaryMiddleware(QuickPayExecuteDelegate next, ILogger<QuickPayLoggerName> logger)
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
                //执行SetNecessary
                context.Request.SetNecessary(context.Config, context.App);
                //重新赋值
                if (context.SignType.IsNullOrWhiteSpace())
                {
                    context.SignType = context.Request.SignTypeName;
                }


                //var methods = context.Request.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
                //var invokeMethods = methods.Where(m => string.Equals(m.Name, "SetNecessary", StringComparison.Ordinal)).ToArray();
                //var methodinfo = invokeMethods[0];

                //if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                //{
                //    var setNecessaryMethod = (Action<AlipayConfig, AlipayApp>)methodinfo.CreateDelegate(typeof(Action<AlipayConfig, AlipayApp>), context.Request);
                //    setNecessaryMethod((AlipayConfig)context.Config, (AlipayApp)context.App);
                //}
                //else
                //{
                //    var setNecessaryMethod = (Action<WechatPayConfig, WechatPayApp>)methodinfo.CreateDelegate(typeof(Action<WechatPayConfig, WechatPayApp>), context.Request);
                //    setNecessaryMethod((WechatPayConfig)context.Config, (WechatPayApp)context.App);
                //}

                Logger.LogDebug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new PayDataTransformError($"设置Necessary发生错误,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
