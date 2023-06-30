using AutoMapper;

using LoggerService.Interfaces;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;

    // Services
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<ISavingsService> _savingsService;
    private readonly Lazy<ISavingsBalanceService> _savingsBalanceService;
    private readonly Lazy<IGroupService> _groupService;
    private readonly Lazy<IGroupMemberService> _groupMemberService;

    public ServiceManager(IConfiguration configuration, ILoggerManager loggerManager, IMapper mapper, IRepositoryManager repositoryManager)
    {
        _configuration = configuration;
        _loggerManager = loggerManager;
        _mapper = mapper;

        _userService = new Lazy<IUserService>(() => new UserService(configuration, mapper, repositoryManager));
        _savingsService = new Lazy<ISavingsService>(() => new SavingsService(configuration, mapper, repositoryManager));
        _savingsBalanceService = new Lazy<ISavingsBalanceService>(() => new SavingsBalanceService(configuration, mapper, repositoryManager));
        _groupService = new Lazy<IGroupService>(() => new GroupService(configuration, mapper, repositoryManager));
        _groupMemberService = new Lazy<IGroupMemberService>(() => new GroupMemberService(configuration, mapper, repositoryManager));
    }

    public IUserService UserService => _userService.Value;

    public ISavingsService SavingsService => _savingsService.Value;

    public ISavingsBalanceService SavingsBalanceService => _savingsBalanceService.Value;

    public IGroupService GroupService => _groupService.Value;

    public IGroupMemberService GroupMemberService => _groupMemberService.Value;
}