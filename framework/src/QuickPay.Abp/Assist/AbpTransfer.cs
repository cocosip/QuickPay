using Abp.Domain.Entities.Auditing;
using DotCommon.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickPay.Assist
{
    /// <summary>AbpTransfer
    /// </summary>
    [AutoMap(typeof(Transfer))]
    [Table("AbpTransfers")]
    public class AbpTransfer : CreationAuditedEntity
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

        /// <summary>微信付款单号/支付宝转账单据号,转账后支付宝或者微信返回的
        /// </summary>
        [StringLength(50)]
        public string TransferNo { get; set; }

        /// <summary>金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>支付状态
        /// </summary>
        [Required]
        public int TransferStatusId { get; set; }

        /// <summary>转账的时间
        /// </summary>
        public DateTime? TransferTime { get; set; }

        /// <summary>参数
        /// </summary>
        public string PayObject { get; set; }

        /// <summary>描述
        /// </summary>
        [StringLength(1024)]
        public string Describe { get; set; }

    }
}
