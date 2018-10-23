using Abp.Domain.Entities.Auditing;
using DotCommon.AutoMapper;
using QuickPay.WechatPay.Authentication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickPay.PayAux
{
    [AutoMap(typeof(JsApiTicket))]
    [Table("AbpJsApiTickets")]
    public class AbpJsApiTicket : CreationAuditedEntity
    {
        [Required]
        [StringLength(50)]
        public string AppId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Ticket { get; set; }

        [Required]
        public int ExpiredIn { get; set; }

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
