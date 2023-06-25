using Common.Models.SavingBalance;

namespace Repository.Contracts
{
    public interface ISavingBalanceRepository
    {
        SavingBalanceModel CreateSavingBalance(SavingBalanceModel savingBalanceModel);
        SavingBalanceModel? SelectById(long savingBalanceId, bool trackChanges = false);
        IEnumerable<SavingBalanceModel> SelectBySavingId(long savingId, bool trackChanges = false);
        void DeleteSavingBalance(SavingBalanceModel savingBalanceModel);
    }
}
