using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Common.LogConfig
{
    public static class LogHelp
    {
        public static ILog Log { get; private set; }

        private static Assembly startupAssembly = Assembly.GetEntryAssembly();
        static LogHelp()
        {
            if (Log == null)
            {
                LoadLog4NetConfig();
            }
        }


        private static void LoadLog4NetConfig()
        {
            var repository = LogManager.CreateRepository(startupAssembly, typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repository, new FileInfo(Directory.GetCurrentDirectory() + "/log4net.config"));
            Log = LogManager.GetLogger(startupAssembly.GetType());

        }

    }
}
