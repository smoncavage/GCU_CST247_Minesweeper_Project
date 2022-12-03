using NLog;
using System;
using System.IO;

namespace Recipe_Shop.Services.Logger
{
    public class MyLogger : ILogger
    {
        private static MyLogger Log;

        private MyLogger() { }

        public static MyLogger getInstance()
        {
            if (Log == null)
            {
                Log = new MyLogger();
            }
            return Log;
        }

        public void Debug(string message)
        {
            File.AppendAllText("C:\\Users\\monca\\source\\repos\\Activity1Part3\\logs\\" + @DateTime.Today.ToString("dd_MMM_yyyy") + "." + "txt", @DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + message + "\n");
        }

        public void Info(string message)
        {
            File.AppendAllText("C:\\Users\\monca\\source\\repos\\Activity1Part3\\logs\\" + @DateTime.Today.ToString("dd_MMM_yyyy") + "." + "txt", @DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + message + "\n");
        }

        public void Warning(string message)
        {
            File.AppendAllText("C:\\Users\\monca\\source\\repos\\Activity1Part3\\logs\\" + @DateTime.Today.ToString("dd_MMM_yyyy") + "." + "txt", @DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + message + "\n");
        }

        public void Error(string message)
        {
            File.AppendAllText("C:\\Users\\monca\\source\\repos\\Activity1Part3\\logs\\" + @DateTime.Today.ToString("dd_MMM_yyyy") + "." + "txt", @DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " - " + message + "\n");
        }

        public MyLogger GetLogger()
        {
            NLog.Logger logger = LogManager.GetLogger("myAppLoggerRules");
            if (Log == null)
            {
                Log = logger;
            }
            return Log;
        }

        public static implicit operator MyLogger(NLog.Logger v)
        {
            throw new NotImplementedException();
        }
    }
}