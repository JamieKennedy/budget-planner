using Common.Models.Savings;

namespace Repository.Contracts
{
    public interface ISavingsRepository
    {
        SavingsModel CreateSavings(SavingsModel createSavingsDto);
        SavingsModel? SelectById(long savingsId, bool trackChanges = false);
    }
}
