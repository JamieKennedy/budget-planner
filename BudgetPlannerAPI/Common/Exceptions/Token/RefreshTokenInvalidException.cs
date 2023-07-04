using Common.Exceptions.Base;

namespace Common.Exceptions.Token
{
    internal class RefreshTokenInvalidException : BadRequestException
    {
        public RefreshTokenInvalidException(string message) : base(message)
        {
        }
    }
}
