using AutoMapper;

using Common.DataTransferObjects.User;
using Common.Models;
using Common.Results.Error.User;

using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Services.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IConfiguration configuration, IMapper mapper, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;

        }

        public async Task<Result> AssignRole(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);
            var result = await _userManager.AddToRoleAsync(user, roleName);

            return Result.OkIf(result.Succeeded, "An error orccured assigning the role");
        }

        public async Task<Result> CreateUser(CreateUserDto createUserDto)
        {
            var userModel = _mapper.Map<User>(createUserDto);
            userModel.Created = DateTime.Now;
            userModel.LastModified = DateTime.Now;

            var result = await _userManager.CreateAsync(userModel, createUserDto.Password);

            //if (_userManager.Users.Count() == 1)
            //{
            //    await _userManager.AddToRoleAsync(userModel, "Admin");
            //}
            //else
            //{
            //    await _userManager.AddToRoleAsync(userModel, "User");
            //}



            return Result.OkIf(result.Succeeded, "An error orccured creating the user");
        }

        public async Task<Result<List<UserDto>>> GetAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }


            var userDtos = _mapper.Map<List<UserDto>>(users) ?? new List<UserDto>();

            return userDtos;
        }

        public async Task<Result<UserDto>> SelectByEmail(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);
            if (user is null) return new UserNotFoundError(emailAddress);


            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<Result<UserDto>> SelectById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }


    }
}
