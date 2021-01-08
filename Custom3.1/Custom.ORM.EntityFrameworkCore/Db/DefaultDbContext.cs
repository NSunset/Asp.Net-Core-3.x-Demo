using Custom.Domain.Entity.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Custom.ORM.EntityFrameworkCore.Configuration.Default;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Custom.lib.IOC;
using Custom.lib.Appsettings;
using Custom.lib.DbContextConfig;

namespace Custom.ORM.EntityFrameworkCore.Db
{
    public class DefaultDbContext : CustomDbContextBase<DefaultDbContext>
    {
        public DbSet<User> User { get; set; }

        private readonly string _connectionStr;

        public DefaultDbContext(
            CustomDbContextOptions<DefaultDbContext> options,
            IDbContextConfiguration dbContextConfiguration,
            AppSetting appSetting) : base(options, dbContextConfiguration)
        {
            _connectionStr = appSetting.ConnectionStrings.Default;
        }
        protected override string ConnectionStr => _connectionStr;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }


    }


}
