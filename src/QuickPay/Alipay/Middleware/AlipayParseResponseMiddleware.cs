using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
using QuickPay.Infrastructure.RequestData;
using QuickPay.Infrastructure.Responses;
using QuickPay.Infrastructure.Util;
using QuickPay.Middleware;
using System.Linq;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    public class AlipayParseResponseMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public AlipayParseResponseMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
            {
                if (context.RequestHandler == QuickPaySettings.RequestHandler.Execute)
                {
                    var payData = new PayData();
                    payData = payData.FromJson(context.HttpResponseString);
                    //获取签名Sign
                    var signKv = payData.GetValue(context.SignFieldName);
                    //数据
                    var responseWapper = payData.GetValues().FirstOrDefault(x => x.Key != context.SignFieldName);

                    var app = (AlipayApp)context.App;
                    var sourceJson = "";
                    if (app.EnableEncrypt)
                    {
                        sourceJson = AlipayUtil.AesDecrypt(app.EncryptKey, responseWapper.ToString(), app.Charset);
                    }
                    else
                    {
                        //未使用加密
                        sourceJson = JsonSerializer.Serialize(responseWapper);
                    }
                    payData = payData.FromJson(sourceJson);
                    payData.SetValue(context.SignFieldName, signKv);
                    //将PayData转换为对象
                    context.Response = (PayResponse)RequestReflectUtil.ToResponse(payData, context.Response.GetType());
                    //ResponsPayData
                    context.ResponsePayData = new PayData(payData.GetValues());
                }
            }
            await _next.Invoke(context);
        }
    }
}
