using DotCommon.Http;
using QuickPay.Middleware;
using QuickPay.WechatPay.Util;
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
            if (context.Request.Provider == QuickPaySettings.Provider.WechatPay)
            {
                if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                {
                    var requestXml = context.RequestPayData.ToXml();
                    var urlProperty = context.Request.GetType().GetProperties(BindingFlags.CreateInstance | BindingFlags.Public).FirstOrDefault(x => x.Name.ToLower() == "requesturl");
                    if (urlProperty == null)
                    {

                    }

                    //发送http请求
                    var builder = RequestBuilder.Instance(urlProperty.GetValue(context.Request).ToString(), RequestConsts.Methods.Post)
                        .SetPost(PostType.Xml, requestXml);
                }
            }
            await _next.Invoke(context);
        }

    }
}
