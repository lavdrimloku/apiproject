using NLog;
using System;
using System.Collections.Generic;
using System.Text;


namespace Services.Logs
{
    public class LogNLog : ILog
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LogNLog()
        {

        }
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Information(string messasges)
        {
            logger.Info(messasges);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }
    }
}
