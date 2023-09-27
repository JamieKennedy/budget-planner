using Common.DataTransferObjects.Income;

namespace Services.Contracts
{
    public interface IIncomeService
    {
        Task<IncomeDto> CreateIncome(Guid userId, CreateIncomeDto createIncomeDto);
        Task<IncomeDto> UpdateIncome(Guid userId, Guid incomeId, UpdateIncomeDto updateIncomeDto);
        Task DeleteIncome(Guid userId, Guid incomeId);
        Task<IncomeDto> SelectById(Guid userId, Guid incomeId, bool trackChanges = false);
        Task<IEnumerable<IncomeDto>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
