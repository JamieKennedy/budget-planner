namespace Services.Contracts;

public interface IServiceManager
{
    IUserService UserService { get; }
    ISavingsService SavingsService { get; }
    ISavingsBalanceService SavingsBalanceService { get; }
    IGroupService GroupService { get; }
    IGroupMemberService GroupMemberService { get; }
}