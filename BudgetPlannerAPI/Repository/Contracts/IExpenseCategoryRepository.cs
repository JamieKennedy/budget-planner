using Common.Models;

namespace Repository.Contracts
{
    public interface IExpenseCategoryRepository
    {
        ExpenseCategory CreateExpenseCategory(ExpenseCategory expenseCategory);
        ExpenseCategory UpdateExpenseCategory(ExpenseCategory expenseCategory);
        void DeleteExpenseCategory(ExpenseCategory expenseCategory);
        ExpenseCategory? SelectById(Guid expenseCategoryId, bool trackChanges = false);
        IEnumerable<ExpenseCategory> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
