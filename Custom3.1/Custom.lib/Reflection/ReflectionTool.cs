using Custom.lib.IOC;
using Custom.lib.Tool;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Custom.lib.Reflection
{
    public class ReflectionTool
    {
        /// <summary>
        /// 获取当前项目需要注入的类型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetCustomRegisterTypes()
        {
            var assemblys = GetCustomAssemblys();
            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes().
                    Where(a =>
                    {
                        var @interfaces = a.GetInterfaces();
                        return
                        !a.IsInterface &&
                        !a.IsAbstract &&
                        (
                            @interfaces.IsAny(typeof(ISingleton))
                            || @interfaces.IsAny(typeof(IScope))
                            || @interfaces.IsAny(typeof(ITransient))
                        );
                    });
                foreach (var type in types)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// 获取当前项目的程序集
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetCustomAssemblys()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyPaths = Directory.GetFiles(directory).Where(path =>
            {
                var dll = path.Split(Path.DirectorySeparatorChar).Last();
                return dll.Contains("Custom") && dll.EndsWith(".dll");
            });
            foreach (var item in assemblyPaths)
            {
                yield return Assembly.Load(AssemblyName.GetAssemblyName(item));
            }
        }
    }
}
