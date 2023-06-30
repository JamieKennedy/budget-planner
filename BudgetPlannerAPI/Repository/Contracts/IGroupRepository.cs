using Common.Models;

namespace Repository.Contracts
{
    public interface IGroupRepository
    {
        Group CreateGroup(Group group);
        Group? SelectById(long groupId, bool trackChanges = false);
    }
}
