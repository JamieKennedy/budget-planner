using LoggerService;
using LoggerService.Interfaces;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Repository;
using Repository.Contracts;

using Services;
using Services.Contracts;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringBase = configuration.GetConnectionString("Default");
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionStringBase)
        {
            UserID = configuration["SqlServer:username"],
            Password = configuration["SqlServer:password"],
        };

        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(connectionStringBuilder.ConnectionString, b => b.MigrationsAssembly("API"));
        });
    }

    public static void ConfigureLoggerManager(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }


}