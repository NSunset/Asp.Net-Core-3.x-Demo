using AutoMapper;
using Custom.lib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Custom.lib.AutoMapperConfig
{
    public class AutoMapperProfile
    {
        public static IEnumerable<Assembly> GetProfileClassChildAssembly()
        {
            var assemblys = ReflectionTool.GetCustomAssemblys();

            var profileAssemblys = assemblys.Where(assembly => assembly.GetTypes().Any(type => type.IsSubclassOf(typeof(Profile))));
            return profileAssemblys;
        }
    }


}
