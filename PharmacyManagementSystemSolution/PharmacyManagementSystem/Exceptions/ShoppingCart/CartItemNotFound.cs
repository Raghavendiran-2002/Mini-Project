using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.ShoppingCart
{
    [Serializable]
    public class CartItemNotFound : Exception
    {
        public CartItemNotFound()
        {
        }

        public CartItemNotFound(string? message) : base(message)
        {
        }

        public CartItemNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CartItemNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}