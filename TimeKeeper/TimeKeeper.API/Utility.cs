using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace TimeKeeper.API
{
    public static class Utility
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(string Message, string level = "ERROR", Exception ex = null)
        {
            if (level == "INFO")
            {
                log.Info(Message);
            }
            else
            {
                log.Error(Message);
            }
        }
    }
}