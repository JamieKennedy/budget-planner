using FluentResults;

using LoggerService.Interfaces;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace LoggerService
{
    public class ResultLogger : IResultLogger
    {
        private readonly ILoggerManager _loggerManager;

        public ResultLogger(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void Log(string context, string content, ResultBase result, LogLevel logLevel)
        {
            var reasons = result.Reasons.Select(reason => reason.Message);
            var logMsg = $"Result: {JsonConvert.SerializeObject(reasons)} {content} <{context}>";

            switch (logLevel)
            {
                case LogLevel.Debug:
                    _loggerManager.LogDebug(logMsg);
                    break;
                case LogLevel.Information:
                    _loggerManager.LogInfo(logMsg);
                    break;
                case LogLevel.Warning:
                    _loggerManager.LogWarning(logMsg);
                    break;
                case LogLevel.Error:
                    _loggerManager.LogError(logMsg);
                    break;
                default:
                    _loggerManager.LogError(logMsg);
                    break;

            }
        }

        public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
        {
            var reasons = result.Reasons.Select(reason => reason.Message);
            var logMsg = $"Result: {reasons} {content} <{typeof(TContext).FullName}>";

            switch (logLevel)
            {
                case LogLevel.Debug:
                    _loggerManager.LogDebug(logMsg);
                    break;
                case LogLevel.Information:
                    _loggerManager.LogInfo(logMsg);
                    break;
                case LogLevel.Warning:
                    _loggerManager.LogWarning(logMsg);
                    break;
                case LogLevel.Error:
                    _loggerManager.LogError(logMsg);
                    break;
                default:
                    _loggerManager.LogError(logMsg);
                    break;

            }
        }
    }
}
