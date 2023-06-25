using Common.DataTransferObjects.SavingBalance;
using Common.Models;

namespace Services.Contracts
{
    public interface ISavingBalanceService
    {
        SavingBalance CreateSavingBalance(CreateSavingBalanceDto createSavingBalanceDto);
        SavingBalance SelectById(long savingBalanceId, bool trackChanges = false);
        IEnumerable<SavingBalance> SelectBySavingId(long savingId, bool trackChanges = false);
        void DeleteSavingBalance(long savingBalanceId);
    }
}
