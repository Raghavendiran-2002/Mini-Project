using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Order
{
    [Serializable]
    public class NoOrderFound : Exception
    {
        public NoOrderFound()
        {
        }

        public NoOrderFound(string? message) : base(message)
        {
        }

        public NoOrderFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoOrderFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}