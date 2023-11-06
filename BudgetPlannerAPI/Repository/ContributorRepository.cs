using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class ContributorRepository : RepositoryBase<Contributor>, IContributorRepository
    {
        public ContributorRepository(RepositoryContext context) : base(context) { }
        public Contributor CreateContributor(Contributor contributer)
        {
            return Create(contributer);
        }

        public void DeleteContributor(Contributor contributer)
        {
            Delete(contributer);
        }

        public Contributor? SelectById(Guid contributorId, bool trackChanges = false)
        {
            return FindByCondition(contributor => contributor.Id == contributorId, trackChanges).FirstOrDefault();
        }

        public List<Contributor> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(contributor => contributor.UserId == userId, trackChanges).ToList();
        }

        public Contributor UpdateContributor(Contributor contributer)
        {
            return Update(contributer);
        }
    }
}
