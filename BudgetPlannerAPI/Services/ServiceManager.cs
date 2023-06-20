using LoggerService.Interfaces;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager {
    private readonly IConfiguration _configuration;
    private readonly ILoggerManager _loggerManager;

    public ServiceManager(IConfiguration configuration, ILoggerManager loggerManager) {
        _configuration = configuration;
        _loggerManager = loggerManager;
    }
}