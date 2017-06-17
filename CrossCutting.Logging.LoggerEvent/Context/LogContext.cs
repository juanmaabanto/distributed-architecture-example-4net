using CatSolution.CrossCutting.Logging.LoggerEvent.Entities;
using System.Data.Entity;

namespace CatSolution.CrossCutting.Logging.LoggerEvent.Context
{
    public class LogContext : DbContext
    {
        public LogContext() : base("LogContext")
        {
        }

        public DbSet<EventLog> EventLogs { get; set; }
    }
}
