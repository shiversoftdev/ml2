using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML2.Core
{
    internal static class Logger
    {
        public delegate string LoggerFunction(string message);
        private const string LogFile = "ML2.log";
#if DEBUG
        public static string LogDebug(string message)
        {
            string msg = $"[Debug]{message}\r\n";
            try
            {
                File.AppendAllText(LogFile, $"[{DateTime.Now.ToUniversalTime()}]{msg}");
            }
            catch { }
            return msg;
        }
#endif

        public static string LogWarning(string message)
        {
            string msg = $"[Warning]{message}\r\n";
            try
            {
                File.AppendAllText(LogFile, $"[{DateTime.Now.ToUniversalTime()}]{msg}");
            }
            catch { }
            return msg;
        }

        public static string LogInfo(string message)
        {
            string msg = $"[Info]{message}\r\n";
            try
            {
                File.AppendAllText(LogFile, $"[{DateTime.Now.ToUniversalTime()}]{msg}");
            }
            catch { }
            return msg;
        }

        private static HashSet<string> LogMessages = new HashSet<string>();
        public static void LogOnce(LoggerFunction logger, string message)
        {
            if(LogMessages.Contains(message) || logger is null)
            {
                return;
            }
            LogMessages.Add(message);
            logger(message);
        }
    }
}
