using Common.Exceptions.Base;

namespace Common.Exceptions.User
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) : base($"No user found with UserId: {userId}") { }
        public UserNotFoundException(string emailAddress) : base($"No user found with email address: {emailAddress}") { }
    }
}
