using Common.Models.Savings;

using Repository.Contracts;

namespace Repository
{
    public class SavingsRepository : RepositoryBase<SavingsModel>, ISavingsRepository
    {
        public SavingsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingsModel CreateSavings(SavingsModel savings)
        {
            return Create(savings);
        }

        public SavingsModel? SelectById(long savingsId, bool trackChanges = false)
        {
            return FindByCondition(savings => savings.SavingsId == savingsId, trackChanges).FirstOrDefault();
        }
    }
}
