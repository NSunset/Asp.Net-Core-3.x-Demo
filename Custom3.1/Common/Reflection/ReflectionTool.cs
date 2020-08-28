using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common.Reflection
{
    public class ReflectionTool
    {
        public static IEnumerable<Type> GetLoadAssemblyTypes()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists(path))
            {
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
            else
            {
                yield return null;
            }
            //AppDomain.CurrentDomain.SetupInformation;
        }
    }
}
