using System;

namespace QuickPay.WechatPay.Authentication
{
    /// <summary>JsApiTicket
    /// </summary>
    public class JsApiTicket
    {
        public string AppId { get; set; }
        public string Ticket { get; set; }
        public int ExpiredIn { get; set; }
        public DateTime LastModifiedTime { get; set; }

        /// <summary>在某个时间下,是否过期
        /// </summary>
        public bool IsExpired(DateTime expiredTime)
        {
            var totalSeconds = (expiredTime - LastModifiedTime).TotalSeconds;
            if (totalSeconds > ExpiredIn)
            {
                return true;
            }
            return false;
        }
    }
}
