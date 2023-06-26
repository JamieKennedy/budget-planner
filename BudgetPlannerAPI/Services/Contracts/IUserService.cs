using Common.DataTransferObjects.User;
using Common.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        User CreateUser(CreateUserDto createUserDto);
        User SelectById(long userId);
        IEnumerable<User> SelectAll();
    }
}
