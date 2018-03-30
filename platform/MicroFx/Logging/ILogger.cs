using System;

namespace MicroFx.Logging
{
    public interface ILogger
    {
        void Error(string message, Exception ex = null);
        void Info(string message, Exception ex = null);
        void Warn(string message, Exception ex = null);
        void ErrorFormat(string format, object[] args);
        void InfoFormat(string format, object[] args);
        void WarnFormat(string format, object[] args);
        void Error(Exception exception);
    }
}