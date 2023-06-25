namespace Services.Contracts;

public interface IServiceManager
{
    IUserService UserService { get; }
    ISavingService SavingService { get; }
    ISavingBalanceService SavingBalanceService { get; }
}