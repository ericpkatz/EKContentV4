using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ekUtilities
{
    public interface ILogProvider
    {
        void Write(string msg, log4net.Core.Level level);
    }
}
