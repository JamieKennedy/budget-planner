using Common.Models;

namespace Repository.Contracts;

public interface IUserRepository
{
    User CreateUser(User user);
    User? SelectById(long userId, bool trackChanges = false);
    IEnumerable<User> SelectAll(bool trackChanges = false);
}
