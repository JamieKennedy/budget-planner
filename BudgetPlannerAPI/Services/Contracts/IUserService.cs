using Common.DataTransferObjects.User;

using FluentResults;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<Result> CreateUser(CreateUserDto createUserDto);
        Task<Result<UserDto>> SelectById(Guid userId);
        Task<Result<UserDto>> SelectByEmail(string emailAddress);
        Task<Result<List<UserDto>>> GetAll();
        Task<Result> AssignRole(Guid userId, string roleName);
    }
}
