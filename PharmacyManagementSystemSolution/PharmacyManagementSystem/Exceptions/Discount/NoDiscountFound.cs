using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Discount
{
    [Serializable]
    public class NoDiscountFound : Exception
    {
        public NoDiscountFound()
        {
        }

        public NoDiscountFound(string? message) : base(message)
        {
        }

        public NoDiscountFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoDiscountFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}