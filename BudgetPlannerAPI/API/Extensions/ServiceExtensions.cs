using Common.Exceptions.Base;

using LoggerService;
using LoggerService.Interfaces;

using Repository;

using Services;
using Services.Contracts;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureLoggerManager(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddSingleton<IServiceManager, ServiceManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddSingleton<IRepositoryManager, RepositoryManager>();
    }
}