using Common.DataTransferObjects.SavingsBalance;

namespace Services.Contracts
{
    public interface ISavingsBalanceService
    {
        SavingsBalanceDto CreateSavingsBalance(long savingsId, CreateSavingsBalanceDto createSavingsBalanceDto);
        SavingsBalanceDto SelectById(long savingsBalanceId, bool trackChanges = false);
        IEnumerable<SavingsBalanceDto> SelectBySavingsId(long savingsId, bool trackChanges = false);
        void DeleteSavingsBalance(long savingsBalanceId);
    }
}
