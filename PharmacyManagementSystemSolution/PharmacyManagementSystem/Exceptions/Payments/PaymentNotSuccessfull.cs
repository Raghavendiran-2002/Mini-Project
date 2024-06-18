using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Payments
{
    [Serializable]
    public class PaymentNotSuccessfull : Exception
    {
        public PaymentNotSuccessfull()
        {
        }

        public PaymentNotSuccessfull(string? message) : base(message)
        {
        }

        public PaymentNotSuccessfull(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PaymentNotSuccessfull(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}