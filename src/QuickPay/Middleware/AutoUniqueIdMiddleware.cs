using DotCommon.Extensions;
using DotCommon.Utility;
using QuickPay.Alipay.Requests;
using QuickPay.Errors;
using System.Threading.Tasks;

namespace QuickPay.Middleware
{
    /// <summary>自动设置请求的UniqueId
    /// </summary>
    public class AutoUniqueIdMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public AutoUniqueIdMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request == null)
            {
                SetPipelineError(context, new SetUniqueIdError("设置UniqueId发生错误"));
                return;
            }
            //自动设置UniqueId与Code
            if (context.Request.UniqueId.IsNullOrWhiteSpace())
            {
                context.Request.UniqueId = ObjectId.GenerateNewStringId();
            }
            if (context.Request.BusinessCode.IsNullOrWhiteSpace())
            {
                context.Request.BusinessCode = QuickPaySettings.DefaultBusinessCode;
            }

            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                //支付宝在转换的时候,BizContent需要自动进行转换
                var property = context.Request.GetType().GetProperty("BizContentRequest");
                var bizContentRequest = property.GetValue(context.Request);
                //设置请求的UniqueId与BusinessCode
                if (!((BaseBizContentRequest)bizContentRequest).UniqueId.IsNullOrWhiteSpace())
                {
                    context.Request.UniqueId = ((BaseBizContentRequest)bizContentRequest).UniqueId;
                }
                if (!((BaseBizContentRequest)bizContentRequest).BusinessCode.IsNullOrWhiteSpace())
                {
                    context.Request.BusinessCode = ((BaseBizContentRequest)bizContentRequest).BusinessCode;
                }
            }
            await _next.Invoke(context);
        }

    }
}
