using AutoMapper;

using Common.Models;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    // Services
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IRoleService> _roleService;
    private readonly Lazy<ISavingsService> _savingsService;
    private readonly Lazy<ISavingsBalanceService> _savingsBalanceService;
    private readonly Lazy<IExpenseCategoryService> _expenseCategoryService;
    private readonly Lazy<IContributorService> _contributorService;
    private readonly Lazy<IAccountService> _accountService;
    private readonly Lazy<IIncomeService> _incomeService;


    public ServiceManager(IConfiguration configuration, ILoggerManager loggerManager, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userService = new Lazy<IUserService>(() => new UserService(configuration, mapper, userManager));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, mapper, userManager, configuration, repositoryManager));
        _roleService = new Lazy<IRoleService>(() => new RoleService(roleManager));
        _savingsService = new Lazy<ISavingsService>(() => new SavingsService(configuration, mapper, repositoryManager, userManager));
        _savingsBalanceService = new Lazy<ISavingsBalanceService>(() => new SavingsBalanceService(configuration, mapper, repositoryManager));
        _expenseCategoryService = new Lazy<IExpenseCategoryService>(() => new ExpenseCategoryService(configuration, mapper, repositoryManager, userManager));
        _contributorService = new Lazy<IContributorService>(() => new ContributorService(configuration, mapper, repositoryManager, userManager));
        _accountService = new Lazy<IAccountService>(() => new AccountService(configuration, mapper, repositoryManager, userManager));
        _incomeService = new Lazy<IIncomeService>(() => new IncomeService(configuration, mapper, repositoryManager, userManager));
    }

    public IUserService UserService => _userService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;

    public ISavingsService SavingsService => _savingsService.Value;

    public ISavingsBalanceService SavingsBalanceService => _savingsBalanceService.Value;

    public IExpenseCategoryService ExpenseCategoryService => _expenseCategoryService.Value;

    public IContributorService ContributorService => _contributorService.Value;

    public IAccountService AccountService => _accountService.Value;

    public IIncomeService IncomeService => _incomeService.Value;

    public IRoleService RoleService => _roleService.Value;
}