using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingsRepository
    {
        Savings CreateSavings(Savings createSavingsDto);
        Savings? SelectById(long savingsId, bool trackChanges = false);
    }
}
