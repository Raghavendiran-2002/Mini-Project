using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Order
{
    [Serializable]
    public class InSufficientStock : Exception
    {
        public InSufficientStock()
        {
        }

        public InSufficientStock(string? message) : base(message)
        {
        }

        public InSufficientStock(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InSufficientStock(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}