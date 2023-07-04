using Common.DataTransferObjects.SavingsBalance;

namespace Services.Contracts
{
    public interface ISavingsBalanceService
    {
        SavingsBalanceDto CreateSavingsBalance(Guid savingsId, CreateSavingsBalanceDto createSavingsBalanceDto);
        SavingsBalanceDto SelectById(Guid savingsBalanceId, bool trackChanges = false);
        IEnumerable<SavingsBalanceDto> SelectBySavingsId(Guid savingsId, bool trackChanges = false);
        void DeleteSavingsBalance(Guid savingsBalanceId);
    }
}
