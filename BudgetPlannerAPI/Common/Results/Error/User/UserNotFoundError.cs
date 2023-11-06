using Common.Results.Error.Base;

namespace Common.Results.Error.User
{
    public class UserNotFoundError : NotFoundError
    {
        public UserNotFoundError(Guid userId) : base($"No user found with UserId: {userId}") { }
        public UserNotFoundError(string emailAddress) : base($"No user found with email address: {emailAddress}") { }
    }
}
