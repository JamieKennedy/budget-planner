using System.Collections.ObjectModel;

using Common.DataTransferObjects.ExpenseCategory;

using FluentResults;

namespace Services.Contracts
{
    public interface IExpenseCategoryService
    {
        Task<Result<ExpenseCategoryDto>> CreateExpenseCategory(Guid userId, CreateExpenseCategoryDto createExpenseCategoryDto);
        Task<Result<ExpenseCategoryDto>> UpdateExpenseCategory(Guid userId, Guid expenseCategoryId, UpdateExpenseCategoryDto updateExpenseCategoryDto);
        Task<Result> DeleteExpenseCategory(Guid userId, Guid expenseCategoryId);
        Task<Result<ExpenseCategoryDto>> SelectById(Guid userId, Guid expenseCategoryId, bool trackChanges = false);
        Task<Result<Collection<ExpenseCategoryDto>>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
