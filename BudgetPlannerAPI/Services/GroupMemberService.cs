using AutoMapper;

using Common.DataTransferObjects.GroupMember;
using Common.Exceptions.Group;
using Common.Exceptions.GroupMember;
using Common.Models;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    internal class GroupMemberService : IGroupMemberService
    {
        private IConfiguration _configuration;
        private IMapper _mapper;
        private IRepositoryManager _repositoryManager;

        public GroupMemberService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public GroupMemberDto CreateGroupMember(long groupId, CreateGroupMemberDto createGroupMemberDto)
        {
            var group = _repositoryManager.Group.SelectById(groupId);

            if (group is null) throw new GroupNotFoundException(groupId);

            var groupMemberModel = _mapper.Map<GroupMember>(createGroupMemberDto);

            var groupMemeber = _repositoryManager.GroupMember.CreateGroupMember(groupMemberModel);
            _repositoryManager.Save();

            var groupMemberDto = _mapper.Map<GroupMemberDto>(groupMemeber);

            return groupMemberDto;
        }

        public IEnumerable<GroupMemberDto> SelectByGroupId(long groupId, bool trackChanges = false)
        {
            var group = _repositoryManager.Group.SelectById(groupId);

            if (group is null) throw new GroupNotFoundException(groupId);

            var groups = _repositoryManager.GroupMember.SelectByGroupId(groupId, trackChanges);

            var groupDtos = _mapper.Map<IEnumerable<GroupMemberDto>>(groups);

            return groupDtos;
        }

        public GroupMemberDto SelectById(long groupMemberId, bool trackChanges = false)
        {
            var groupMember = _repositoryManager.GroupMember.SelectById(groupMemberId);

            if (groupMember is null) throw new GroupMemberNotFoundException(groupMemberId);

            var groupMemberDto = _mapper.Map<GroupMemberDto>(groupMember);

            return groupMemberDto;
        }
    }
}
