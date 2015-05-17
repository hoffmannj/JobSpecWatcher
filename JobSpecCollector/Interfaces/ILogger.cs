using System;

namespace JobSpecCollector.Interfaces
{
    public interface ILogger
    {
        void Write(string content);
        void Write(Exception exception);
    }
}
