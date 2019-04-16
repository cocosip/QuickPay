namespace QuickPay.Infrastructure.Apps
{
    /// <summary>支付应用抽象类
    /// </summary>
    public abstract class QuickPayApp
    {
        /// <summary>管道名称
        /// </summary>
        public abstract string Provider { get; }
    }
}
