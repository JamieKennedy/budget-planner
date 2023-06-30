using Common.DataTransferObjects.GroupMember;

namespace Services.Contracts
{
    public interface IGroupMemberService
    {
        GroupMemberDto CreateGroupMember(long groupId, CreateGroupMemberDto createGroupMemberDto);
        GroupMemberDto SelectById(long groupMemberId, bool trackChanges = false);
        IEnumerable<GroupMemberDto> SelectByGroupId(long groupId, bool trackChanges = false);
    }
}
