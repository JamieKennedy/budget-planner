using AutoMapper;

using Common.DataTransferObjects.Group;
using Common.Exceptions.Group;
using Common.Exceptions.User;
using Common.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    internal class GroupService : IGroupService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public GroupService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<GroupDto> CreateGroup(Guid userId, CreateGroupDto createGroupDto)
        {
            _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);
            var groupModel = _mapper.Map<Group>(createGroupDto);
            groupModel.GroupOwnerId = userId;

            var group = _repositoryManager.Group.CreateGroup(groupModel);
            _repositoryManager.Save();

            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }

        public GroupDto SelectById(Guid groupId, bool trackChanges = false)
        {
            var group = _repositoryManager.Group.SelectById(groupId, trackChanges) ?? throw new GroupNotFoundException(groupId);
            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }
    }
}
