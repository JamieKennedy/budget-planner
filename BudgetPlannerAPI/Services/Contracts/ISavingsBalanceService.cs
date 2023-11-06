using Common.DataTransferObjects.SavingsBalance;

using FluentResults;

namespace Services.Contracts
{
    public interface ISavingsBalanceService
    {
        Result<SavingsBalanceDto> CreateSavingsBalance(Guid savingsId, CreateSavingsBalanceDto createSavingsBalanceDto);
        Result<SavingsBalanceDto> SelectById(Guid savingsBalanceId, bool trackChanges = false);
        Result<List<SavingsBalanceDto>> SelectBySavingsId(Guid savingsId, bool trackChanges = false);
        Result DeleteSavingsBalance(Guid savingsBalanceId);
    }
}
