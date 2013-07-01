using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ekUtilities
{
    public class Log4NetProvider : ILogProvider
    {
        log4net.ILog log4 = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Write(string msg, log4net.Core.Level level)
        {
            if (level == log4net.Core.Level.Warn)
                log4.Warn(msg);
            else if (level == log4net.Core.Level.Debug)
                log4.Debug(msg);
            else if (level == log4net.Core.Level.Error)
                log4.Error(msg);
            else if (level == log4net.Core.Level.Info)
                log4.Info(msg);
            
        }
    }
}
