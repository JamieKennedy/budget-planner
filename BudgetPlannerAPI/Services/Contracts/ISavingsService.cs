using Common.DataTransferObjects.Savings;

using FluentResults;

namespace Services.Contracts
{
    public interface ISavingsService
    {
        Task<Result<SavingsDto>> CreateSavings(Guid userId, CreateSavingsDto createSavingsDto);
        Result<SavingsDto> SelectById(Guid savingsId);
        Task<Result<List<SavingsDto>>> SelectByUserId(Guid userId, bool trackChanges = false);
        Task<Result> DeleteById(Guid userId, Guid savingsId);
        Task<Result<SavingsDto>> UpdateSavings(Guid userId, Guid savingsId, UpdateSavingsDto updateSavingsDto);
    }
}
