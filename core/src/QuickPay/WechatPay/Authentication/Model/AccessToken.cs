using System;

namespace QuickPay.WechatPay.Authentication
{
    public class AccessToken
    {
        public string AppId { get; set; }
        public string Token { get; set; }
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

        public override string ToString()
        {
            return $"[AppId:{AppId},Token:{Token},ExpiredIn:{ExpiredIn},LastModifiedTime:{LastModifiedTime.ToString("yyyy-MM-dd HH:mm:ss")}]";
        }
    }
}
