namespace QuickPay
{
    /// <summary>SqlServer配置信息
    /// </summary>
    public class QuickPaySqlServerOption
    {
        /// <summary>数据库连接字符串
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>支付存储的表名
        /// </summary>
        public string PaymentTableName { get; set; } = "QP_Payments";

        /// <summary>退款存储的表名
        /// </summary>
        public string RefundTableName { get; set; } = "QP_Refunds";

        /// <summary>转账存储的表名
        /// </summary>
        public string TransferTableName { get; set; } = "QP_Transfers";
    }
}
