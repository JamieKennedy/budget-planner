using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingsRepository
    {
        Savings CreateSavings(Savings savings);
        Savings? SelectById(Guid savingsId, bool trackChanges = false);
        List<Savings> SelectByUserId(Guid userId, bool trackChanges = false);
        void DeleteSavings(Savings savings);

        Savings UpdateSavings(Savings savings);
    }
}
