using System;
using JobSpecCollector.Interfaces;

namespace JobSpecCollector.Implementations
{
    public class Logger : ILogger
    {
        private Simplify.Log.Logger _logger;

        public Logger()
        {
            _logger = new Simplify.Log.Logger();
        }

        public void Write(Exception exception)
        {
            _logger.Write(exception);
        }

        public void Write(string content)
        {
            _logger.Write(content);
        }
    }
}
