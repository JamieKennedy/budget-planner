using Common.Models;

namespace Repository.Contracts
{
    public interface IGroupMemberRepository
    {
        GroupMember CreateGroupMember(GroupMember member);
        GroupMember? SelectById(Guid groupMemberId, bool trackChanges = false);
        IEnumerable<GroupMember> SelectByGroupId(Guid groupId, bool trackChanges = false);
    }
}
