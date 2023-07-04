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

        public async Task<IdentityResult> CreateUser(CreateUserDto createUserDto)
        {
            var userModel = _mapper.Map<User>(createUserDto);

            var result = await _userManager.CreateAsync(userModel, createUserDto.Password);

            return result;
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
