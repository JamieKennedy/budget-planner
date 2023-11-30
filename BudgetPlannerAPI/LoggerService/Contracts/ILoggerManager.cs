namespace LoggerService.Interfaces;

public interface ILoggerManager
{
    void LogDebug(string message);
    void LogDebug(string tempalte, params object?[]? propertyValues);
    void LogInfo(string message);
    void LogInfo(string tempalte, params object?[]? propertyValues);
    void LogWarning(string message);
    void LogWarning(string tempalte, params object?[]? propertyValues);
    void LogError(Exception exception);
    void LogError(string message);
    void LogError(string tempalte, params object?[]? propertyValues);
}