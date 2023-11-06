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
            return FindByCondition(savings => savings.Id == savingsId, trackChanges).Include(x => x.SavingsBalances).FirstOrDefault();
        }

        public List<Savings> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(saving => saving.UserId == userId, trackChanges).Include(x => x.SavingsBalances).ToList();
        }

        public Savings UpdateSavings(Savings savings)
        {
            return Update(savings);
        }
    }
}
