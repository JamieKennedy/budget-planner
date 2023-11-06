using Common.Results.Error.Base;

namespace Common.Results.Error.Token
{
    public class RefreshTokenInvalidError : BadRequestError
    {
        public RefreshTokenInvalidError(string message) : base(message)
        {
        }
    }
}
