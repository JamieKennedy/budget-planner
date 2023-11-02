using Common.DataTransferObjects.User;

using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(CreateUserDto createUserDto);
        Task<UserDto> SelectById(Guid userId);
        Task<UserDto> SelectByEmail(string emailAddress);
        Task<List<UserDto>> GetAll();
        Task<IdentityResult> AssignRole(Guid userId, string roleName);
    }
}
