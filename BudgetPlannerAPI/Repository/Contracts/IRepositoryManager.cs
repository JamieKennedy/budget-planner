namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    ISavingsRepository Savings { get; }
    ISavingsBalanceRepository SavingsBalance { get; }
    IGroupRepository Group { get; }
    IGroupMemberRepository GroupMember { get; }

    void Save();
}