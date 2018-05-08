namespace QuickPay.Errors
{
    public class PayDataTransformError : Error
    {
        public PayDataTransformError(string message) : base(message, (int)QuickPayErrorCodes.PayDataTransform)
        {

        }
    }
}
