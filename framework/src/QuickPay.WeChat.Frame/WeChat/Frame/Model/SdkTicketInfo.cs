using System;

namespace QuickPay.WeChat.Frame.Model
{
    /// <summary>微信SdkTicket存储实体
    /// </summary>
    [Serializable]
    public class SdkTicketInfo
    {
        /// <summary>租户Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>Ticket
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>过期时间
        /// </summary>
        public int ExpiredIn { get; set; }

        /// <summary>最后修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>临时票据类型
        /// </summary>
        public string TicketType { get; set; }

        /// <summary>在某个时间下,是否过期
        /// </summary>
        public bool IsExpired(DateTime expiredTime)
        {
            var totalSeconds = (expiredTime - UpdateTime).TotalSeconds;
            if (totalSeconds > ExpiredIn)
            {
                return true;
            }
            return false;
        }

        /// <summary>Override ToString
        /// </summary>
        public override string ToString()
        {
            return $"[TenantId:{TenantId},AppId:{AppId},Ticket:{Ticket},ExpiredIn:{ExpiredIn},UpdateTime:{UpdateTime:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}
