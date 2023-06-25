namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    ISavingRepository Saving { get; }
    ISavingBalanceRepository SavingBalance { get; }

    void Save();
}