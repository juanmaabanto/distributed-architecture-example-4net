using CatSolution.CrossCutting.Logging.LoggerEvent.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatSolution.CrossCutting.Logging.LoggerEvent
{
    public class Logger : ILogger, IDisposable
    {
        private EventLogRepository _repository = null;

        public Logger()
        {
            _repository = new EventLogRepository();
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Info(string message)
        {
            return Info(message, string.Empty, string.Empty);
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Info(string message, string source)
        {
            return Info(message, source, string.Empty);
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public LoggerResult Info(string message, string source, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = _repository.RegisterEvent(EventLogTypes.Information, message, source, string.Empty, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> InfoAsync(string message)
        {
            var result = await InfoAsync(message, string.Empty, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> InfoAsync(string message, string source)
        {
            var result = await InfoAsync(message, source, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type info.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="message">Source to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public async Task<LoggerResult> InfoAsync(string message, string source, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = await _repository.RegisterEventAsync(EventLogTypes.Information, message, source, string.Empty, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Warning(string message)
        {
            return Warning(message, string.Empty, string.Empty);
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Warning(string message, string source)
        {
            return Warning(message, source, string.Empty);
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public LoggerResult Warning(string message, string source, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = _repository.RegisterEvent(EventLogTypes.Warning, message, source, string.Empty, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> WarningAsync(string message)
        {
            var result = await WarningAsync(message, string.Empty, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> WarningAsync(string message, string source)
        {
            var result = await WarningAsync(message, source, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type warning.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public async Task<LoggerResult> WarningAsync(string message, string source, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = await _repository.RegisterEventAsync(EventLogTypes.Warning, message, source, string.Empty, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Error(string message)
        {
            return Error(message, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Error(string message, string source)
        {
            return Error(message, source, string.Empty, string.Empty);
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="messageException">Detail Message to be recorded.</param>
        /// <returns></returns>
        public LoggerResult Error(string message, string source, string messageException)
        {
            return Error(message, source, messageException, string.Empty);
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="messageException">Detail Message to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public LoggerResult Error(string message, string source, string messageException, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = _repository.RegisterEvent(EventLogTypes.Error, message, source, messageException, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> ErrorAsync(string message)
        {
            var result = await ErrorAsync(message, string.Empty, string.Empty, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> ErrorAsync(string message, string source)
        {
            var result = await ErrorAsync(message, source, string.Empty, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="messageException">Details Message to be recorded.</param>
        /// <returns></returns>
        public async Task<LoggerResult> ErrorAsync(string message, string source, string messageException)
        {
            var result = await ErrorAsync(message, source, messageException, string.Empty);
            return result;
        }

        /// <summary>
        /// Logs an event type error.
        /// </summary>
        /// <param name="message">Message to be recorded.</param>
        /// <param name="source">Source to be recorded.</param>
        /// <param name="messageException">Details Message to be recorded.</param>
        /// <param name="userName">User generating the event.</param>
        /// <returns></returns>
        public async Task<LoggerResult> ErrorAsync(string message, string source, string messageException, string userName)
        {
            LoggerResult result = null;
            IEnumerable<string> errors = null;

            try
            {
                bool success = await _repository.RegisterEventAsync(EventLogTypes.Error, message, source, messageException, userName);

                if (success)
                {
                    result = new LoggerResult(true);
                }
                else
                {
                    errors = new List<string>() { "an error while trying to register the event has occurred." };
                    result = new LoggerResult(errors);
                }
            }
            catch (Exception ex)
            {
                errors = new List<string>() { ex.Message, ex.StackTrace };
                result = new LoggerResult(errors);
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }
        }

    }
}
