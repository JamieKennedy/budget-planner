using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class IncomeRepository : RepositoryBase<Income>, IIncomeRepository
    {
        public IncomeRepository(RepositoryContext context) : base(context) { }
        public Income CreateIncome(Income income)
        {
            return Create(income);
        }

        public void DeleteIncome(Income income)
        {
            Delete(income);
        }

        public Income? SelectById(Guid id, bool trackChanges = false)
        {
            return FindByCondition(income => income.Id == id, trackChanges).FirstOrDefault();
        }

        public List<Income> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(income => income.UserId == userId, trackChanges).ToList();
        }

        public Income UpdateIncome(Income income)
        {
            return Update(income);
        }
    }
}
