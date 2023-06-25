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
    private readonly Lazy<ISavingService> _savingService;
    private readonly Lazy<ISavingBalanceService> _savingBalanceService;

    public ServiceManager(IConfiguration configuration, ILoggerManager loggerManager, IMapper mapper, IRepositoryManager repositoryManager)
    {
        _configuration = configuration;
        _loggerManager = loggerManager;
        _mapper = mapper;

        _userService = new Lazy<IUserService>(() => new UserService(configuration, mapper, repositoryManager));
        _savingService = new Lazy<ISavingService>(() => new SavingService(configuration, mapper, repositoryManager));
        _savingBalanceService = new Lazy<ISavingBalanceService>(() => new SavingBalanceService(configuration, mapper, repositoryManager));
    }

    public IUserService UserService => _userService.Value;

    public ISavingService SavingService => _savingService.Value;

    public ISavingBalanceService SavingBalanceService => _savingBalanceService.Value;
}