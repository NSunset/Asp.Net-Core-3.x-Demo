using Common.LogConfig;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Common.Reflection
{
    public class ReflectionTool
    {
        public static IEnumerable<Type> GetLoadAssemblyTypes()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var paths = Directory.GetFiles(path).Where(x => x.Contains(".dll"));

            foreach (var item in paths)
            {
                var types = System.Reflection.Assembly.LoadFile(item).GetTypes().
                    Where(a => a.GetInterfaces().Any(@interface => @interface == typeof(Common.IOC.ISingleton) || @interface == typeof(Common.IOC.IScope) || @interface == typeof(Common.IOC.ITransient)));
                foreach (var type in types)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Assembly> GetLoadAssembly()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var paths = Directory.GetFiles(path).Where(x => x.Contains(".dll"));
            LogHelp.Log.Debug("程序集:\r\n" + string.Join("\r\n", paths));
            foreach (var item in paths)
            {
                yield return Assembly.Load(AssemblyName.GetAssemblyName(item));
            }
        }
    }
}
