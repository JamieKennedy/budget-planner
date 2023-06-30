using Common.Models;

namespace Repository.Contracts
{
    public interface IGroupMemberRepository
    {
        GroupMember CreateGroupMember(GroupMember member);
        GroupMember? SelectById(long groupMemberId, bool trackChanges = false);
        IEnumerable<GroupMember> SelectByGroupId(long groupId, bool trackChanges = false);
    }
}
