using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class SavingsBalanceRepository : RepositoryBase<SavingsBalance>, ISavingsBalanceRepository
    {
        public SavingsBalanceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingsBalance CreateSavingsBalance(SavingsBalance savingsBalanceModel)
        {
            return Create(savingsBalanceModel);
        }

        public void DeleteSavingsBalance(SavingsBalance savingsBalanceModel)
        {
            Delete(savingsBalanceModel);
        }

        public SavingsBalance? SelectById(Guid savingsBalanceId, bool trackChanges = false)
        {
            return FindByCondition(savingsBalance => savingsBalance.Id == savingsBalanceId, trackChanges).FirstOrDefault();
        }

        public List<SavingsBalance> SelectBySavingsId(Guid savingsId, bool trackChanges = false)
        {
            return FindByCondition(savingsBalance => savingsBalance.SavingsId == savingsId, trackChanges).ToList();
        }
    }
}
