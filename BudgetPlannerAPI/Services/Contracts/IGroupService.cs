using Common.DataTransferObjects.Group;

namespace Services.Contracts
{
    public interface IGroupService
    {
        Task<GroupDto> CreateGroup(Guid userId, CreateGroupDto createGroupDto);
        GroupDto SelectById(Guid groupId, bool trackChanges = false);
    }
}
