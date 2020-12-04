using AutoMapper;
using Common.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.AutoMapperConfig
{
    public class AutoMapperProfile
    {
        public static IEnumerable<Assembly> GetProfileClassChildAssembly()
        {
            var assemblys = ReflectionTool.GetLoadAssembly();

            var profileAssemblys = assemblys.Where(assembly => assembly.GetName().Name != nameof(AutoMapper) && assembly.GetTypes().Any(type => type.IsSubclassOf(typeof(Profile))));
            return profileAssemblys;
        }
    }


}
