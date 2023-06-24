using Common.Models.Savings;
using Common.Models.Savings.Dto;

namespace Services.Contracts
{
    public interface ISavingsService
    {
        SavingsModel CreateSavings(CreateSavingsDto createSavingsDto);
        SavingsModel SelectById(long savingsId);
    }
}
