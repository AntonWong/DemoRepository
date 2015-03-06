using System;
using System.Configuration;
using System.Globalization;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "configs/log4net.config", Watch = true)]
namespace Be.MikeBevers.Logging
{
    public class Logger : ILogger
    {
        #region Datamembers

        private static ILog log = null;
        private const string Log4netConnectionString = "Log4netConnectionString";

        #endregion

        #region Class Initializer

        public Logger()
        {
            log = LogManager.GetLogger(typeof(Logger));
            log4net.GlobalContext.Properties["host"] = Environment.MachineName;
        }

        #endregion

        #region ILogger Members

        public void EnterMethod(string methodName)
        {
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "Entering Method {0}", methodName));
        }

        public void LeaveMethod(string methodName)
        {
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "Leaving Method {0}", methodName));
        }

        public void LogException(Exception exception)
        {
            if (log.IsErrorEnabled)
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", exception.Message), exception);
        }
        public void LogException(string message, Exception exception)
        {
            if (log.IsErrorEnabled)
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", message), exception);
        }

        public void LogError(string message)
        {
            if (log.IsErrorEnabled)
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }

        public void LogWarningMessage(string message)
        {
            if (log.IsWarnEnabled)
                log.Warn(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }

        public void LogInfoMessage(string message)
        {
            if (log.IsInfoEnabled)
                log.Info(string.Format(CultureInfo.InvariantCulture, "{0}", message));
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public void WriteLog(Type t, Exception ex)
        {
            ILog log = LogManager.GetLogger(t);
            log.Info("日志：", ex);
        }
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public void WriteLog(Type t, string msg)
        {
            ILog log = LogManager.GetLogger(t);
            log.Info(msg);
        }
        #endregion 
    }
}

