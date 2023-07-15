using Common.Models;

namespace Repository.Contracts
{
    public interface IContributorRepository
    {
        Contributor CreateContributor(Contributor contributer);
        Contributor UpdateContributor(Contributor contributer);
        void DeleteContributor(Contributor contributer);
        Contributor? SelectById(Guid contributorId, bool trackChanges = false);
        IEnumerable<Contributor> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
