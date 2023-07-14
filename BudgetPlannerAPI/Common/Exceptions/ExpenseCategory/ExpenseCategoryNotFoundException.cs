using Common.Exceptions.Base;

namespace Common.Exceptions.ExpenseCategory
{
    public class ExpenseCategoryNotFoundException : NotFoundException
    {
        public ExpenseCategoryNotFoundException(Guid expenseCategoryId) : base($"No Expense Category found with Id: {expenseCategoryId}")
        {
        }
    }
}
