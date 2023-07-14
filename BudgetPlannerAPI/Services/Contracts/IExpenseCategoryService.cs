using Common.DataTransferObjects.ExpenseCategory;

namespace Services.Contracts
{
    public interface IExpenseCategoryService
    {
        Task<ExpenseCategoryDto> CreateExpenseCategory(Guid userId, CreateExpenseCategoryDto createExpenseCategoryDto);
        Task<ExpenseCategoryDto> UpdateExpenseCategory(Guid userId, Guid expenseCategoryId, UpdateExpenseCategoryDto updateExpenseCategoryDto);
        void DeleteExpenseCategory(Guid userId, Guid expenseCategoryId);
        Task<ExpenseCategoryDto> SelectById(Guid userId, Guid expenseCategoryId, bool trackChanges = false);
        Task<IEnumerable<ExpenseCategoryDto>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
