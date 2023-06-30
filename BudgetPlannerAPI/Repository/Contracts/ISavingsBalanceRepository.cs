using Common.Models;

namespace Repository.Contracts
{
    public interface ISavingsBalanceRepository
    {
        SavingsBalance CreateSavingsBalance(SavingsBalance savingsBalanceModel);
        SavingsBalance? SelectById(long savingsBalanceId, bool trackChanges = false);
        IEnumerable<SavingsBalance> SelectBySavingsId(long savingsId, bool trackChanges = false);
        void DeleteSavingsBalance(SavingsBalance savingsBalanceModel);
    }
}
