using Common.Models.SavingBalance;

using Repository.Contracts;

namespace Repository
{
    public class SavingBalanceRepository : RepositoryBase<SavingBalanceModel>, ISavingBalanceRepository
    {
        public SavingBalanceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public SavingBalanceModel CreateSavingBalance(SavingBalanceModel savingBalanceModel)
        {
            return Create(savingBalanceModel);
        }

        public void DeleteSavingBalance(SavingBalanceModel savingBalanceModel)
        {
            Delete(savingBalanceModel);
        }

        public SavingBalanceModel? SelectById(long savingBalanceId, bool trackChanges = false)
        {
            return FindByCondition(savingBalance => savingBalance.SavingBalanceId == savingBalanceId, trackChanges).FirstOrDefault();
        }

        public IEnumerable<SavingBalanceModel> SelectBySavingId(long savingId, bool trackChanges = false)
        {
            return FindByCondition(savingBalance => savingBalance.SavingId == savingId, trackChanges);
        }
    }
}
