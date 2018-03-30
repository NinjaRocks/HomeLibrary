using DbUp.Engine.Output;
using MicroFx.Logging;

namespace MicroFx.Data.Migration
{
    public class LogProvider : IUpgradeLog
    {
        private static readonly ILogger logger = LogManager.GetLogger(typeof(LogProvider));

        public void WriteInformation(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }
    }
}