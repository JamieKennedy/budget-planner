namespace LoggerService.Interfaces;

public interface ILoggerManager
{
    void LogDebug(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}