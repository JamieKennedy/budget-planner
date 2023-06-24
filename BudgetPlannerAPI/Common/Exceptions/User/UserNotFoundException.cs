using Common.Exceptions.Base;

namespace Common.Exceptions.User
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(long userId) : base($"No user found with UserId: {userId}") { }
    }
}
