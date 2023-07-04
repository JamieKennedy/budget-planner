using System.Runtime.Serialization;

namespace Services
{
    [Serializable]
    internal class RefreshTokenInvalidException : Exception
    {
        public RefreshTokenInvalidException()
        {
        }

        public RefreshTokenInvalidException(string? message) : base(message)
        {
        }

        public RefreshTokenInvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RefreshTokenInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}