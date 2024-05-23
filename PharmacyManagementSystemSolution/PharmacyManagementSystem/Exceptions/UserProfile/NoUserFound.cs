using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.UserProfile
{
    [Serializable]
    public class NoUserFound : Exception
    {
        public NoUserFound()
        {
        }

        public NoUserFound(string? message) : base(message)
        {
        }

        public NoUserFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoUserFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}