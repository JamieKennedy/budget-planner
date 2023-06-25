using Common.Models;
using Repository.Contracts;

namespace Repository
{
    public class SavingBalanceRepository : RepositoryBase<SavingBalance>, ISavingBalanceRepository
    {
        public SavingBalanceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingBalance CreateSavingBalance(SavingBalance savingBalanceModel)
        {
            return Create(savingBalanceModel);
        }

        public void DeleteSavingBalance(SavingBalance savingBalanceModel)
        {
            Delete(savingBalanceModel);
        }

        public SavingBalance? SelectById(long savingBalanceId, bool trackChanges = false)
        {
            return FindByCondition(savingBalance => savingBalance.SavingBalanceId == savingBalanceId, trackChanges).FirstOrDefault();
        }

        public IEnumerable<SavingBalance> SelectBySavingId(long savingId, bool trackChanges = false)
        {
            return FindByCondition(savingBalance => savingBalance.SavingId == savingId, trackChanges);
        }
    }
}
