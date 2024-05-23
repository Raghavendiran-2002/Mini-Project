using System.Runtime.Serialization;

namespace PharmacyManagementSystem.Exceptions.General
{
    [Serializable]
    internal class ItemCannotBeNull : Exception
    {
        public ItemCannotBeNull()
        {
        }

        public ItemCannotBeNull(string? message) : base(message)
        {
        }

        public ItemCannotBeNull(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemCannotBeNull(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}