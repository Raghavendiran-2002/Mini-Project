using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.UserProfile
{
    [Serializable]
    public  class PasswordIncorrect : Exception
    {
        public PasswordIncorrect()
        {
        }

        public PasswordIncorrect(string? message) : base(message)
        {
        }

        public PasswordIncorrect(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PasswordIncorrect(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}