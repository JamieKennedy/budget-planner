using AutoMapper;

using Common.DataTransferObjects.User;
using Common.Exceptions.User;
using Common.Models;

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

        public async Task<IdentityResult> AssignRole(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result;
        }

        public async Task<IdentityResult> CreateUser(CreateUserDto createUserDto)
        {
            var userModel = _mapper.Map<User>(createUserDto);

            var result = await _userManager.CreateAsync(userModel, createUserDto.Password);

            if (_userManager.Users.Count() == 1)
            {
                await _userManager.AddToRoleAsync(userModel, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(userModel, "User");
            }



            return result;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }


            var userDtos = _mapper.Map<List<UserDto>>(users) ?? new List<UserDto>();

            return userDtos;
        }

        public async Task<UserDto> SelectByEmail(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress) ?? throw new UserNotFoundException(emailAddress);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> SelectById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }


    }
}
