using Common.Models.Saving;

using Repository.Contracts;

namespace Repository
{
    public class SavingRepository : RepositoryBase<SavingModel>, ISavingRepository
    {
        public SavingRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingModel CreateSaving(SavingModel saving)
        {
            return Create(saving);
        }

        public SavingModel? SelectById(long savingId, bool trackChanges = false)
        {
            return FindByCondition(saving => saving.SavingId == savingId, trackChanges).FirstOrDefault();
        }
    }
}
