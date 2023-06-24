namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    ISavingsRepository Savings { get; }

    void Save();
}