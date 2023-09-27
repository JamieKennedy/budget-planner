using Common.Models;

namespace Repository.Contracts
{
    public interface IIncomeRepository
    {
        Income CreateIncome(Income income);
        Income UpdateIncome(Income income);
        void DeleteIncome(Income income);
        Income? SelectById(Guid id, bool trackChanges = false);
        IEnumerable<Income> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
