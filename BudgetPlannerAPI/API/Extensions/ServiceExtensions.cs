using LoggerService;
using LoggerService.Interfaces;

namespace API.Extensions;

public static class ServiceExtensions {
    public static void ConfigureLoggerManager(this IServiceCollection services) {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}