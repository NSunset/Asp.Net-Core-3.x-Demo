using System;
using System.Collections.Generic;
using System.Text;
using Custom.lib.IOC;
using Microsoft.Extensions.Configuration;

namespace Custom.lib.Appsettings
{
    public class AppSetting : ISingleton, IDependencyInterfaceIgnore
    {
        public AppSetting(IConfiguration configuration)
        {
            configuration.Bind(this);
        }

        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
    }

    public class Connectionstrings
    {
        public const string ConnectionStr = "ConnectionStrings";

        public string Initial { get; set; }
        public string Default { get; set; }
    }

}
