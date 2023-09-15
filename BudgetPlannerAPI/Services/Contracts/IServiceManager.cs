namespace Services.Contracts;

public interface IServiceManager
{
    IUserService UserService { get; }
    IAuthenticationService AuthenticationService { get; }
    ISavingsService SavingsService { get; }
    ISavingsBalanceService SavingsBalanceService { get; }
    IExpenseCategoryService ExpenseCategoryService { get; }
    IContributorService ContributorService { get; }
    IAccountService AccountService { get; }

}