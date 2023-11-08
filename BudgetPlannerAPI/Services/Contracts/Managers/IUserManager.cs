using Common.Models;

using FluentResults;

namespace Services.Contracts.Managers
{
    public interface IUserManager
    {
        public Result<User> CreateUser(User user, string password);
        public Result<User> UpdateUser(User user);
        public Result DeleteUser(User user);
        public Result<User> FindUser(Guid userId, bool trackChanges = false);
        public Result<User> FindUser(string emailAddress, bool trackChanges = false);
        public Result VerifyPassword(User user, string password);
        public Result IncrementAccessFailedCount(User user);
        public Result<List<User>> GetAll(bool trackChanges = false);
    }
}
