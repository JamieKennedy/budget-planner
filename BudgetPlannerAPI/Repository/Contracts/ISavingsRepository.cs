using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingsRepository
    {
        Savings CreateSavings(Savings savings);
        Savings? SelectById(Guid savingsId, bool trackChanges = false);
    }
}
