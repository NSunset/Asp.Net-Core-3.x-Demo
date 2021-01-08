using Custom.lib.IOC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.DbContextConfig
{
    public class CustomDbContextOptions<TContext> : DbContextOptions<TContext>, IScope, IDependencyInterfaceIgnore where TContext : DbContext
    {
    }
}
