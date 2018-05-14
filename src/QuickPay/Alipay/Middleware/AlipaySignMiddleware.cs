using QuickPay.Alipay.Apps;
using QuickPay.Alipay.Util;
using QuickPay.Errors;
using QuickPay.Infrastructure.Requests;
using QuickPay.Middleware;
using System;
using System.Threading.Tasks;

namespace QuickPay.Alipay.Middleware
{
    /// <summary>支付宝签名组件
    /// </summary>
    public class AlipaySignMiddleware : QuickPayMiddleware
    {
        private readonly QuickPayExecuteDelegate _next;
        public AlipaySignMiddleware(QuickPayExecuteDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(ExecuteContext context)
        {
            try
            {
                if (context.Request.Provider == QuickPaySettings.Provider.Alipay)
                {
                    var app = (AlipayApp)context.App;
                    var bizContentField = "biz_content";
                    //开启加密
                    if (app.EnableEncrypt)
                    {
                        //未设置BizContent
                        if (!context.RequestPayData.IsSet(bizContentField))
                        {
                            SetPipelineError(context, new SignError("支付宝加密biz_content未设置"));
                            return;
                        }
                        var encryptedContent = AlipayUtil.AesEncrypt(app.EncryptKey, context.RequestPayData.GetValue(bizContentField).ToString(), app.Charset);
                        //加密后的数据替换未加密的
                        context.RequestPayData.SetValue(bizContentField, encryptedContent);
                        context.RequestPayData.SetValue("encrypt_type", app.EncryptType);
                    }
                    //等待签名字符串
                    var signContent = AlipaySignature.GetSignContent(context.RequestPayData.GetValues());
                    //签名
                    var sign = AlipaySignature.RSASign(signContent, app.PrivateKey, app.Charset, app.SignType);
                    //将签名添加到数据
                    context.RequestPayData.SetValue(context.SignFieldName, sign);
                    Logger.Info(context.Request.GetLogFormat($"等待签名字符串:[{signContent}],签名:[{sign}],签名后完整数据:{context.RequestPayData.ToJson()}"));

                    //模块
                    Logger.Debug(context.Request.GetLogFormat($"模块:{MiddlewareName}执行."));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(context.Request.GetLogFormat($"支付宝签名发生错误,{ex.Message}"));
                SetPipelineError(context, new SignError("支付宝签名发生错误"));
                return;
            }
            await _next.Invoke(context);
        }

    }
}
