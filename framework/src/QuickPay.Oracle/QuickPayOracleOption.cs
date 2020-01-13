﻿namespace QuickPay
{
    /// <summary>SqlServer配置信息
    /// </summary>
    public class QuickPayOracleOption
    {
        /// <summary>数据库连接字符串
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>Schema
        /// </summary>
        public string Schema { get; set; } = "quickpay";

        /// <summary>支付存储的表名
        /// </summary>
        public string PaymentTableName { get; set; } = "PAYMENTS";

        /// <summary>退款存储的表名
        /// </summary>
        public string RefundTableName { get; set; } = "REFUNDS";

        /// <summary>转账存储的表名
        /// </summary>
        public string TransferTableName { get; set; } = "TRANSFERS";
    }
}
