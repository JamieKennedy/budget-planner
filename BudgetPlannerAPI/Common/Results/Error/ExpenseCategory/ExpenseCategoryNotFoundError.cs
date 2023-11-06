using Common.Results.Error.Base;

namespace Common.Results.Error.ExpenseCategory
{
    public class ExpenseCategoryNotFoundError : NotFoundError
    {
        public ExpenseCategoryNotFoundError(Guid expenseCategoryId) : base($"No Expense Category found with Id: {expenseCategoryId}")
        {
        }
    }
}
