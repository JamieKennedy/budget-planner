using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingsBalanceRepository
    {
        SavingsBalance CreateSavingsBalance(SavingsBalance savingsBalanceModel);
        SavingsBalance? SelectById(Guid savingsBalanceId, bool trackChanges = false);
        IEnumerable<SavingsBalance> SelectBySavingsId(Guid savingsId, bool trackChanges = false);
        void DeleteSavingsBalance(SavingsBalance savingsBalanceModel);
    }
}
