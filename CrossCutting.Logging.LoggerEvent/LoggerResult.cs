using System.Collections.Generic;

namespace CatSolution.CrossCutting.Logging.LoggerEvent
{
    public class LoggerResult
    {
        private bool _succeeded;

        private IEnumerable<string> _errors;

        public LoggerResult(IEnumerable<string> error)
        {
            _succeeded = false;
            _errors = error;
        }

        public LoggerResult(bool success)
        {
            _succeeded = true;
        }

        public bool Succeeded
        {
            get
            {
                return _succeeded;
            }
        }

        public IEnumerable<string> Errors
        {
            get
            {
                return _errors;
            }
        }
        
    }
}
