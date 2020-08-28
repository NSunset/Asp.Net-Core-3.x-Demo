using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.AppSettings
{
    public class ConnectionString
    {
        public const string ConnectionStr = "ConnectionStrings";

        public string Initial { get; set; }

        public string Default { get; set; }
    }
}
