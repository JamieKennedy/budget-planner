using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class ExpenseCategoryRepository : RepositoryBase<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public ExpenseCategory CreateExpenseCategory(ExpenseCategory expenseCategory)
        {
            return Create(expenseCategory);
        }

        public void DeleteExpenseCategory(ExpenseCategory expenseCategory)
        {
            Delete(expenseCategory);
        }

        public ExpenseCategory? SelectById(Guid expenseCategoryId, bool trackChanges = false)
        {
            return FindByCondition(category => category.Id.Equals(expenseCategoryId), trackChanges).FirstOrDefault();
        }

        public List<ExpenseCategory> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(category => category.UserId.Equals(userId), trackChanges).ToList();
        }

        public ExpenseCategory UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            return Update(expenseCategory);
        }
    }
}
