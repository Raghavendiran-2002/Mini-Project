using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Products
{
    [Serializable]
    public class NoProductFoundByName : Exception
    {
        public NoProductFoundByName()
        {
        }

        public NoProductFoundByName(string? message) : base(message)
        {
        }

        public NoProductFoundByName(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoProductFoundByName(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}