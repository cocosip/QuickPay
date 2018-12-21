namespace QuickPay.Notify
{
    /// <summary>抽象通知
    /// </summary>
    public abstract class NotifyBase : INotify
    {

    }

    /// <summary>泛型抽象通知
    /// </summary>
    public abstract class NotifyBase<T, R> : NotifyBase
    {

    }
}
