using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Category
{
    [Serializable]
    public class NoCategoryFound : Exception
    {
        public NoCategoryFound()
        {
        }

        public NoCategoryFound(string? message) : base(message)
        {
        }

        public NoCategoryFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoCategoryFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}