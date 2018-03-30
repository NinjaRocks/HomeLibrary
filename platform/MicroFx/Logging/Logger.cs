using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace MicroFx.Logging
{
    public class Logger: ILogger
    {
        private readonly ILog log;

        public Logger(string source)
        {
            log = log4net.LogManager.GetLogger(source);
        }

        public void Error(string message, Exception ex = null)
        {
            if(ex!=null) log.Error(message, ex);
            else log.Error(message);
        }

        public void Info(string message, Exception ex = null)
        {
            if (ex != null) log.Info(message, ex);
            else log.Info(message);
        }

        public void Warn(string message, Exception ex = null)
        {
            if (ex != null) log.Warn(message, ex);
            else log.Warn(message);
        }

        public void ErrorFormat(string format, object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public void InfoFormat(string format, object[] args)
        {
            log.InfoFormat(format, args);
        }

        public void WarnFormat(string format, object[] args)
        {
            log.WarnFormat(format, args);
        }

        public void Error(Exception exception)
        {
            log.Error(exception);
        }
    }
}
