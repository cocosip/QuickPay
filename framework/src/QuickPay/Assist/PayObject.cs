namespace QuickPay.Assist
{
    /// <summary>支付/退款等参数基类
    /// </summary>
    public abstract class PayObjectBase
    {
        /// <summary>唯一Id
        /// </summary>
        public string UniqueId { get; set; }
    }

    /// <summary>泛型PayObject
    /// </summary>
    public class PayObject<TObject> : PayObjectBase
    {
        /// <summary>Ctor
        /// </summary>
        public TObject Object { get; set; }

        /// <summary>Ctor
        /// </summary>
        public PayObject()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public PayObject(TObject @object)
        {
            Object = @object;
        }
    }

    /// <summary>PayObject
    /// </summary>
    public class PayObject : PayObject<object>
    {
        /// <summary>Ctor
        /// </summary>
        public PayObject() : base()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public PayObject(object @object) : base(@object)
        {

        }
    }
}
