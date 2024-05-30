using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Review
{
    [Serializable]
    public class UserNotPurchasedProduct : Exception
    {
        public UserNotPurchasedProduct()
        {
        }

        public UserNotPurchasedProduct(string? message) : base(message)
        {
        }

        public UserNotPurchasedProduct(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotPurchasedProduct(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}