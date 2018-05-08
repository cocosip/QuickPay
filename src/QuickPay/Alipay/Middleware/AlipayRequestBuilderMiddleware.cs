using DotCommon.Http;
using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
using QuickPay.Middleware;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    public class AlipayRequestBuilderMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public AlipayRequestBuilderMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                {
                    var app = (AlipayApp)context.App;
                    var config = (AlipayConfig)context.Config;
                    //转换成请求的数据
                    var requestStr = AlipayUtil.BuildQuery(context.RequestPayData.GetValues(), app.Charset);
                    //发送http请求
                    var builder = RequestBuilder.Instance(config.Gateway, RequestConsts.Methods.Post)
                        //.SetUrlEncode()
                        .SetPost(PostType.FormUrlEncoded, requestStr);
                }
            }
            await _next.Invoke(context);
        }


    }
}
