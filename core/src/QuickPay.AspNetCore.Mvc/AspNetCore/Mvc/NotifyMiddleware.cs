using DotCommon.AspNetCore.Mvc.Extensions;
using DotCommon.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace QuickPay.AspNetCore.Mvc
{
    public class NotifyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<QuickPayLoggerName> _logger;
        // private readonly INotifyHandlerManager _notifyHandlerManager;

        public NotifyMiddleware(RequestDelegate next, ILogger<QuickPayLoggerName> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            //判断是否为Notify地址
            // var notify = _notifyManager.GetNotifyByRelateUrl(context.Request.Path);
            // if (notify == null)
            // {
            //     await _next.Invoke(context);
            // }
            // var notifyBody = await context.Request.GetRawBodyStringAsync();
            // _logger.LogDebug("接收到了服务器异步通知:[{0}]", notifyBody);
            // try
            // {
            //     //进行校验,是否来自微信或者支付宝服务器
            //     if (await notify.IsRealNotify(notifyBody))
            //     {

            //     }
            //     else
            //     {
            //         _logger.LogInformation("接收服务器异步通知出错,签名验证失败!");
            //     }
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError("接收异步通知发生错误,错误信息为:{0},接收到的消息:[通知服务器:{1},数据:【{2}】]", ex.Message, context.Request.GetRemoteIpAddress(), notifyBody);
            // }

        }


    }
}
