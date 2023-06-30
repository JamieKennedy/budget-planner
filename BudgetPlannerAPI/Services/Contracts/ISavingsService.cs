using Common.DataTransferObjects.Savings;

namespace Services.Contracts
{
    public interface ISavingsService
    {
        SavingsDto CreateSavings(long userId, CreateSavingsDto createSavingsDto);
        SavingsDto SelectById(long savingsId);
    }
}
