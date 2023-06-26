using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class SavingsBalanceRepository : RepositoryBase<SavingsBalance>, ISavingsBalanceRepository
    {
        public SavingsBalanceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingsBalance CreateSavingsBalance(long savingsId, SavingsBalance savingsBalanceModel)
        {
            savingsBalanceModel.SavingsId = savingsId;
            return Create(savingsBalanceModel);
        }

        public void DeleteSavingsBalance(SavingsBalance savingsBalanceModel)
        {
            Delete(savingsBalanceModel);
        }

        public SavingsBalance? SelectById(long savingsBalanceId, bool trackChanges = false)
        {
            return FindByCondition(savingsBalance => savingsBalance.SavingsBalanceId == savingsBalanceId, trackChanges).FirstOrDefault();
        }

        public IEnumerable<SavingsBalance> SelectBySavingsId(long savingsId, bool trackChanges = false)
        {
            return FindByCondition(savingsBalance => savingsBalance.SavingsId == savingsId, trackChanges);
        }
    }
}
