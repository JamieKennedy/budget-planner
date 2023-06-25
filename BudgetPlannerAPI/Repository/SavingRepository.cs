using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class SavingRepository : RepositoryBase<Saving>, ISavingRepository
    {
        public SavingRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public Saving CreateSaving(Saving saving)
        {
            return Create(saving);
        }

        public Saving? SelectById(long savingId, bool trackChanges = false)
        {
            return FindByCondition(saving => saving.SavingId == savingId, trackChanges).FirstOrDefault();
        }
    }
}
