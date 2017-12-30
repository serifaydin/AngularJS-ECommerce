using System.IO;

namespace Web_EgitimAngularJS.ServiceManager
{
    public class LoggerManager
    {
        private static LoggerManager _loggerManager;
        static object _lockObject = new object();
        private LoggerManager() { }

        public static LoggerManager CreateAsSingleton()
        {
            lock (_lockObject)
                return _loggerManager ?? (_loggerManager = new LoggerManager());
        }

        public bool LogEkleMethot(string path, string mesaj)
        {
            LoggerProcessManager _manager = new LoggerProcessManager(new LoggerTextService());
            return _manager.LogEkle(path, mesaj);
        }
    }

    abstract class LoggerFactory
    {
        public abstract void Log(string path, string message);
    }

    class LoggerTextService : LoggerFactory
    {
        public override void Log(string path, string message)
        {
            FileStream fileStream = new FileStream(path, FileMode.Append);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(message);
            writer.Flush();
            fileStream.Close();
        }
    }

    class LoggerProcessManager
    {
        private LoggerFactory loggerFactory;

        public LoggerProcessManager(LoggerFactory _loggerFactory)
        {
            loggerFactory = _loggerFactory;
        }

        public bool LogEkle(string path, string message)
        {
            bool durum = true;

            try
            {
                loggerFactory.Log(path, message);
            }
            catch
            {
                durum = false;
            }

            return durum;
        }
    }
}