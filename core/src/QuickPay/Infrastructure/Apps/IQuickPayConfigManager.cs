namespace QuickPay.Infrastructure.Apps
{
    public interface IQuickPayConfigManager
    {
        QuickPayConfig GetCurrentConfig(string provider);
    }
}
