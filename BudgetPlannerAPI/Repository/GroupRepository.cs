using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Group CreateGroup(Group group)
        {
            return Create(group);
        }

        public Group? SelectById(long groupId, bool trackChanges = false)
        {
            return FindByCondition(group => group.GroupId == groupId, trackChanges).FirstOrDefault();
        }
    }
}
