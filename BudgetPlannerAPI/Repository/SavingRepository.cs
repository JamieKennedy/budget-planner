using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class SavingsRepository : RepositoryBase<Savings>, ISavingsRepository
    {
        public SavingsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public Savings CreateSavings(Savings savings)
        {
            return Create(savings);
        }

        public Savings? SelectById(long savingsId, bool trackChanges = false)
        {
            return FindByCondition(savings => savings.SavingsId == savingsId, trackChanges).FirstOrDefault();
        }
    }
}
