using Common.Models.SavingBalance;
using Common.Models.SavingBalance.Dto;

namespace Services.Contracts
{
    public interface ISavingBalanceService
    {
        SavingBalanceModel CreateSavingBalance(CreateSavingBalanceDto createSavingBalanceDto);
        SavingBalanceModel SelectById(long savingBalanceId, bool trackChanges = false);
        IEnumerable<SavingBalanceModel> SelectBySavingId(long savingId, bool trackChanges = false);
        void DeleteSavingBalance(long savingBalanceId);
    }
}
