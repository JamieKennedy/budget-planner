using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class GroupMemberRepository : RepositoryBase<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public GroupMember CreateGroupMember(GroupMember member)
        {
            return Create(member);
        }

        public IEnumerable<GroupMember> SelectByGroupId(Guid groupId, bool trackChanges = false)
        {
            return FindByCondition(groupMember => groupMember.GroupId == groupId, trackChanges);
        }

        public GroupMember? SelectById(Guid groupMemberId, bool trackChanges = false)
        {
            return FindByCondition(groupMember => groupMember.GroupMemberId == groupMemberId, trackChanges).FirstOrDefault();
        }
    }
}
