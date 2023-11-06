using Common.DataTransferObjects.Income;

using FluentResults;

namespace Services.Contracts
{
    public interface IIncomeService
    {
        Task<Result<IncomeDto>> CreateIncome(Guid userId, CreateIncomeDto createIncomeDto);
        Task<Result<IncomeDto>> UpdateIncome(Guid userId, Guid incomeId, UpdateIncomeDto updateIncomeDto);
        Task<Result> DeleteIncome(Guid userId, Guid incomeId);
        Task<Result<IncomeDto>> SelectById(Guid userId, Guid incomeId, bool trackChanges = false);
        Task<Result<List<IncomeDto>>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
