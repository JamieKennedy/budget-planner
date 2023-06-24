using AutoMapper;

using Common.Exceptions.User;
using Common.Models.User;
using Common.Models.User.Dto;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public UserModel CreateUser(CreateUserDto createUserDto)
        {
            var userModel = _mapper.Map<UserModel>(createUserDto);

            var user = _repositoryManager.User.CreateUser(userModel);
            _repositoryManager.Save();

            return user;
        }

        public UserModel SelectById(long userId)
        {
            var user = _repositoryManager.User.SelectById(userId);

            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            return user;
        }
    }
}
