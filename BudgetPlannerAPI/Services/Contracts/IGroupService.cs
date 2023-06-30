using Common.DataTransferObjects.Group;

namespace Services.Contracts
{
    public interface IGroupService
    {
        GroupDto CreateGroup(long userId, CreateGroupDto createGroupDto);
        GroupDto SelectById(long groupId, bool trackChanges = false);
    }
}
