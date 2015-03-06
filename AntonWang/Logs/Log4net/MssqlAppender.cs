using System;
using System.Configuration;
using log4net.Appender;

namespace Be.MikeBevers.Logging
{
    public class MssqlAppender : AdoNetAppender
    {
        private const string Log4netConnectionString = "Log4netConnectionString";

        public new string ConnectionString 
        {
            get { return base.ConnectionString; }
            set { base.ConnectionString = ConfigurationManager.ConnectionStrings[Log4netConnectionString].ConnectionString; }
        }
    }
}
