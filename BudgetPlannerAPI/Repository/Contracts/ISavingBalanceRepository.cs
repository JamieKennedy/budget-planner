using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingBalanceRepository
    {
        SavingBalance CreateSavingBalance(SavingBalance savingBalanceModel);
        SavingBalance? SelectById(long savingBalanceId, bool trackChanges = false);
        IEnumerable<SavingBalance> SelectBySavingId(long savingId, bool trackChanges = false);
        void DeleteSavingBalance(SavingBalance savingBalanceModel);
    }
}
