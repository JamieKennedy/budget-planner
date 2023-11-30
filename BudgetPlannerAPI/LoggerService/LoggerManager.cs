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
        Serilog.Debugging.SelfLog.Enable(Console.Error);
    }

    public void LogDebug(string message)
    {
        _logger.Debug("{Message}", message);
    }

    public void LogDebug(string tempalte, params object?[]? propertyValues)
    {
        _logger.Debug(tempalte, propertyValues);
    }

    public void LogInfo(string message)
    {
        _logger.Information("{Message}", message);
    }

    public void LogInfo(string tempalte, params object?[]? propertyValues)
    {
        _logger.Information(tempalte, propertyValues);
    }

    public void LogWarning(string message)
    {
        _logger.Warning("{Message}", message);
    }

    public void LogWarning(string tempalte, params object?[]? propertyValues)
    {
        _logger.Warning(tempalte, propertyValues);
    }

    public void LogError(Exception exception)
    {
        _logger.Error(exception, exception.Message);
    }

    public void LogError(string message)
    {
        _logger.Error("{Message}", message);
    }

    public void LogError(string template, params object?[]? propertyValues)
    {
        _logger.Error(template, propertyValues);
    }
}