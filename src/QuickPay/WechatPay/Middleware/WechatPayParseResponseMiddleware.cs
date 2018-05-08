using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using QuickPay.WechatPay.Util;
using System.Threading.Tasks;

namespace QuickPay.WechatPay.Middleware
{
    public class WechatPayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public WechatPayParseResponseMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
            {
                if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                {
                    var payData = new PayData();
                    payData = payData.FromXml(context.HttpResponseString);
                    context.Response = (PayResponse)RequestReflectUtil.ToResponse(payData, context.Request.GetType());
                    context.ResponsePayData = new PayData(payData.GetValues());
                }
            }
            await _next.Invoke(context);
        }
    }
}
