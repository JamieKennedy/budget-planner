using Common.DataTransferObjects.Savings;

namespace Services.Contracts
{
    public interface ISavingsService
    {
        SavingsDto CreateSavings(CreateSavingsDto createSavingsDto);
        SavingsDto SelectById(long savingsId);
    }
}
