using Common.DataTransferObjects.Savings;

namespace Services.Contracts
{
    public interface ISavingsService
    {
        Task<SavingsDto> CreateSavings(Guid userId, CreateSavingsDto createSavingsDto);
        SavingsDto SelectById(Guid savingsId);
        Task<IEnumerable<SavingsDto>> SelectByUserId(Guid userId, bool trackChanges = false);
        Task DeleteById(Guid userId, Guid savingsId);
        Task<SavingsDto> UpdateSavings(Guid userId, Guid savingsId, UpdateSavingsDto updateSavingsDto);
    }
}
