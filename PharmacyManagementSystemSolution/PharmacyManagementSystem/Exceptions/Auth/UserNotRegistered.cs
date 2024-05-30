using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.Auth
{
    [Serializable]
    public class UserNotRegistered : Exception
    {
        public UserNotRegistered()
        {
        }

        public UserNotRegistered(string? message) : base(message)
        {
        }

        public UserNotRegistered(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotRegistered(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}