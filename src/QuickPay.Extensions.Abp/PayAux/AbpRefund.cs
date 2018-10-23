using Abp.Domain.Entities.Auditing;
using DotCommon.AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickPay.PayAux
{
    [AutoMap(typeof(Refund))]
    [Table("AbpRefunds")]
    public class AbpRefund : CreationAuditedEntity
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

        /// <summary>交易编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string OutTradeNo { get; set; }

        /// <summary>交易号(支付宝或者微信系统)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TransactionId { get; set; }

        /// <summary>本系统退款编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string OutRefundNo { get; set; }

        /// <summary>退款金额
        /// </summary>
        [Required]
        public decimal RefundAmount { get; set; }

        /// <summary>微信或者支付宝退款号
        /// </summary>
        [StringLength(50)]
        public string RefundId { get; set; }


        /// <summary>参数
        /// </summary>
        public string PayObject { get; set; }

        /// <summary>描述
        /// </summary>
        [StringLength(1024)]
        public string Describe { get; set; }
    }
}
