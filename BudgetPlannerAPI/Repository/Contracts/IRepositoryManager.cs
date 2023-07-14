namespace Repository.Contracts;

public interface IRepositoryManager
{
    ITokenRepository Tokens { get; }
    ISavingsRepository Savings { get; }
    ISavingsBalanceRepository SavingsBalance { get; }

    void Save();
}