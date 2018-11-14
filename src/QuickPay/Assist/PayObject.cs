namespace QuickPay.Assist
{
    /// <summary>支付/退款等参数基类
    /// </summary>
    public abstract class PayObjectBase
    {
        public string UniqueId { get; set; }
    }

    public class PayObject<TObject> : PayObjectBase
    {
        public TObject Object { get; set; }

        public PayObject()
        {

        }

        public PayObject(TObject @object)
        {
            Object = @object;
        }
    }

    public class PayObject : PayObject<object>
    {
        public PayObject() : base()
        {

        }
        public PayObject(object @object) : base(@object)
        {

        }
    }
}
