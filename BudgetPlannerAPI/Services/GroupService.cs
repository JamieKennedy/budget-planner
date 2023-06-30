using AutoMapper;

using Common.DataTransferObjects.Group;
using Common.Exceptions.Group;
using Common.Exceptions.User;
using Common.Models;

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

        public GroupService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public GroupDto CreateGroup(long userId, CreateGroupDto createGroupDto)
        {
            _ = _repositoryManager.User.SelectById(userId) ?? throw new UserNotFoundException(userId);
            var groupModel = _mapper.Map<Group>(createGroupDto);
            groupModel.GroupOwnerId = userId;

            var group = _repositoryManager.Group.CreateGroup(groupModel);
            _repositoryManager.Save();

            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }

        public GroupDto SelectById(long groupId, bool trackChanges = false)
        {
            var group = _repositoryManager.Group.SelectById(groupId, trackChanges) ?? throw new GroupNotFoundException(groupId);
            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }
    }
}
