using AutoMapper;

using Common.DataTransferObjects.User;
using Common.Exceptions.User;
using Common.Models;

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

        public User CreateUser(CreateUserDto createUserDto)
        {
            var userModel = _mapper.Map<User>(createUserDto);

            var user = _repositoryManager.User.CreateUser(userModel);
            _repositoryManager.Save();

            return user;
        }

        public IEnumerable<User> SelectAll()
        {
            return _repositoryManager.User.SelectAll(false);
        }

        public User SelectById(long userId)
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
