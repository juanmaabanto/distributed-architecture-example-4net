using System;
using System.ComponentModel.DataAnnotations;

namespace CatSolution.CrossCutting.Logging.LoggerEvent.Entities
{
    public class EventLog
    {
        [Key]
        public int EventLogId { get; set; }

        public EventLogTypes EventLogTypeId { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string MessageException { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

    }
}
