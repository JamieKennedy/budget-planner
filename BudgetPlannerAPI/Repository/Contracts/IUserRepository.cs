using Common.Models.User;

namespace Repository.Contracts;

public interface IUserRepository
{
    UserModel CreateUser(UserModel user);
    UserModel? SelectById(long userId, bool trackChanges = false);
}
