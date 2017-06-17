using System.Threading.Tasks;

namespace CatSolution.CrossCutting.Logging.LoggerEvent
{
    public interface ILogger
    {
        LoggerResult Info(string message);

        LoggerResult Info(string message, string source);

        LoggerResult Info(string message, string source, string userName);

        Task<LoggerResult> InfoAsync(string message);

        Task<LoggerResult> InfoAsync(string message, string source);

        Task<LoggerResult> InfoAsync(string message, string source, string userName);

        LoggerResult Warning(string message);

        LoggerResult Warning(string message, string source);

        LoggerResult Warning(string message, string source, string userName);

        Task<LoggerResult> WarningAsync(string message);

        Task<LoggerResult> WarningAsync(string message, string source);

        Task<LoggerResult> WarningAsync(string message, string source, string userName);

        LoggerResult Error(string message);

        LoggerResult Error(string message, string source);

        LoggerResult Error(string message, string source, string messageException);

        LoggerResult Error(string message, string source, string messageException, string userName);

        Task<LoggerResult> ErrorAsync(string message);

        Task<LoggerResult> ErrorAsync(string message, string source);

        Task<LoggerResult> ErrorAsync(string message, string source, string messageException);

        Task<LoggerResult> ErrorAsync(string message, string source, string messageException, string userName);
    }
}
