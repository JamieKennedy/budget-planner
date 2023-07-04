using Common.DataTransferObjects.GroupMember;

namespace Services.Contracts
{
    public interface IGroupMemberService
    {
        GroupMemberDto CreateGroupMember(Guid groupId, CreateGroupMemberDto createGroupMemberDto);
        GroupMemberDto SelectById(Guid groupMemberId, bool trackChanges = false);
        IEnumerable<GroupMemberDto> SelectByGroupId(Guid groupId, bool trackChanges = false);
    }
}
