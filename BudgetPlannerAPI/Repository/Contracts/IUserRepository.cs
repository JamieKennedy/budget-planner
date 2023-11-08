using Common.Models;

namespace Repository.Contracts
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(User user);
        User? SelectById(Guid userId, bool trackChanges = false);
        User? SelectByEmail(string email, bool trackChanges = false);
        List<User> SelectAll(bool trackChanges = false);

    }
}
