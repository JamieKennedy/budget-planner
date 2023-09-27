namespace Repository.Contracts;

public interface IRepositoryManager
{
    ITokenRepository Tokens { get; }
    ISavingsRepository Savings { get; }
    ISavingsBalanceRepository SavingsBalance { get; }
    IExpenseCategoryRepository ExpenseCategory { get; }
    IContributorRepository Contributor { get; }
    IAccountRepository Account { get; }
    IIncomeRepository Income { get; }
    void Save();
}