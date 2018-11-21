using Abp.Domain.Entities.Auditing;
using DotCommon.AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickPay.Assist
{
    [AutoMap(typeof(Payment))]
    [Table("AbpPayments")]
    public class AbpPayment : CreationAuditedEntity
    {
        /// <summary>唯一Id
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UniqueId { get; set; }

        /// <summary>支付平台
        /// </summary>
        [Required]
        public int PayPlatId { get; set; }

        /// <summary>应用Id(支付宝或者微信的AppId)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string AppId { get; set; }

        /// <summary>交易编号(本系统)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string OutTradeNo { get; set; }

        /// <summary>交易类型
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TradeType { get; set; }

        /// <summary>业务代码
        /// </summary>
        [Required]
        [StringLength(20)]
        public string BusinessCode { get; set; }

        /// <summary>交易号(支付宝或者微信系统)
        /// </summary>
        [StringLength(50)]
        public string TransactionId { get; set; }

        /// <summary>金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>支付状态
        /// </summary>
        [Required]
        public int PayStatusId { get; set; }

        /// <summary>参数
        /// </summary>
        public string PayObject { get; set; }

        /// <summary>描述
        /// </summary>
        [StringLength(1024)]
        public string Describe { get; set; }
    }
}
