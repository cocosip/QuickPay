using QuickPay.Alipay.Apps;
using QuickPay.Errors;
using QuickPay.Infrastructure.Apps;
using QuickPay.Infrastructure.Requests;
using QuickPay.WechatPay.Apps;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    public class SetNecessaryMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;

        public SetNecessaryMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                var methods = context.Request.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m => string.Equals(m.Name, "SetNecessary", StringComparison.Ordinal)).ToArray();
                var methodinfo = invokeMethods[0];

                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {
                    var setNecessaryMethod = (Action<AlipayConfig, AlipayApp>)methodinfo.CreateDelegate(typeof(Action<AlipayConfig, AlipayApp>), context.Request);
                    setNecessaryMethod((AlipayConfig)context.Config, (AlipayApp)context.App);
                }
                else
                {
                    var setNecessaryMethod = (Action<WechatPayConfig, WechatPayApp>)methodinfo.CreateDelegate(typeof(Action<WechatPayConfig, WechatPayApp>), context.Request);
                    setNecessaryMethod((WechatPayConfig)context.Config, (WechatPayApp)context.App);
                }

                // await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"设置Necessary发生错误,{ex.Message}"));
                SetPipelineError(context, new PayDataTransformError("设置Necessary发生错误"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
