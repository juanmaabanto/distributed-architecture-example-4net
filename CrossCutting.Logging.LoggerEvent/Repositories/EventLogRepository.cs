using CatSolution.CrossCutting.Logging.LoggerEvent.Context;
using CatSolution.CrossCutting.Logging.LoggerEvent.Entities;
using System;
using System.Threading.Tasks;

namespace CatSolution.CrossCutting.Logging.LoggerEvent.Repositories
{
    public class EventLogRepository : IEventLogRepository, IDisposable
    {
        private LogContext _ctx = null;

        public EventLogRepository()
        {
            _ctx = new LogContext();
        }

        public bool RegisterEvent(EventLogTypes eventLogTypes, string message, string source, string messageException, string userName)
        {
            EventLog eventLog = new EventLog()
            {
                EventLogTypeId = eventLogTypes,
                Source = source,
                Message = message,
                MessageException = messageException,
                UserName = userName,
                Date = DateTime.UtcNow
            };

            _ctx.EventLogs.Add(eventLog);
            int c = _ctx.SaveChanges();

            return (c == 0 ? false : true);
        }

        public async Task<bool> RegisterEventAsync(EventLogTypes eventLogTypes, string message, string source, string messageException, string userName)
        {
            EventLog eventLog = new EventLog()
            {
                EventLogTypeId = eventLogTypes,
                Source = source,
                Message = message,
                MessageException = messageException,
                UserName = userName,
                Date = DateTime.UtcNow
            };

            _ctx.EventLogs.Add(eventLog);
            int c = await _ctx.SaveChangesAsync();

            return (c == 0 ? false : true);
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
                if (_ctx != null)
                {
                    _ctx.Dispose();
                    _ctx = null;
                }
            }
        }
    }
}
