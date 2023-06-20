using LoggerService.Interfaces;

using Microsoft.Extensions.Configuration;

using Serilog;

namespace LoggerService;

public class LoggerManager : ILoggerManager
{
    private readonly ILogger _logger;

    public LoggerManager(IConfiguration configuration)
    {
        _logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
    }

    public void LogDebug(string message)
    {
        _logger.Debug("{Message}", message);
    }

    public void LogInfo(string message)
    {
        _logger.Information("{Message}", message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning("{Message}", message);
    }

    public void LogError(string message)
    {
        _logger.Error("{Message}", message);
    }
}