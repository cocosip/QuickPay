using Abp.Domain.Entities.Auditing;
using DotCommon.AutoMapper;
using QuickPay.WeChatPay.Authentication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickPay.Assist
{
    /// <summary>AbpJsApiTickets
    /// </summary>
    [AutoMap(typeof(JsApiTicket))]
    [Table("AbpJsApiTickets")]
    public class AbpJsApiTicket : CreationAuditedEntity
    {
        /// <summary>AppId
        /// </summary>
        [Required]
        [StringLength(50)]
        public string AppId { get; set; }

        /// <summary>Ticket
        /// </summary>
        [Required]
        [StringLength(1024)]
        public string Ticket { get; set; }

        /// <summary>过期时间
        /// </summary>
        [Required]
        public int ExpiredIn { get; set; }

        /// <summary>最后修改时间
        /// </summary>
        [Required]
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
