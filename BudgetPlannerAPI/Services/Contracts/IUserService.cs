using Common.Models.User;
using Common.Models.User.Dto;

namespace Services.Contracts
{
    public interface IUserService
    {
        UserModel CreateUser(CreateUserDto createUserDto);
        UserModel SelectById(long userId);
    }
}
