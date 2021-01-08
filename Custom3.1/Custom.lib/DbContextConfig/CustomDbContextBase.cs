using Custom.lib.Appsettings;
using Custom.lib.IOC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.DbContextConfig
{
    public abstract class CustomDbContextBase<TDbContext> : DbContext, IScope, IDependencyInterfaceIgnore where TDbContext : DbContext
    {
        protected readonly IDbContextConfiguration _dbContextConfiguration;

        protected CustomDbContextBase(
            CustomDbContextOptions<TDbContext> options,
            IDbContextConfiguration dbContextConfiguration) : base(options)
        {
            this._dbContextConfiguration = dbContextConfiguration;
        }

        protected abstract string ConnectionStr { get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _dbContextConfiguration.Configure<TDbContext>(optionsBuilder, ConnectionStr);
        }


    }
}
