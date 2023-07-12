using Common.Models;

using Microsoft.EntityFrameworkCore;

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

        public void DeleteSavings(Savings savings)
        {
            Delete(savings);
        }

        public Savings? SelectById(Guid savingsId, bool trackChanges = false)
        {
            return FindByCondition(savings => savings.SavingsId == savingsId, trackChanges).FirstOrDefault();
        }

        public IEnumerable<Savings>? SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(saving => saving.UserId == userId, trackChanges).Include(x => x.SavingsBalances);
        }

        public Savings UpdateSavings(Savings savings)
        {
            return Update(savings);
        }
    }
}
