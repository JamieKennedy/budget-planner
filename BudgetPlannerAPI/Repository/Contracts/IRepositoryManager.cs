namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    ISavingsRepository Savings { get; }
    ISavingsBalanceRepository SavingsBalance { get; }

    void Save();
}