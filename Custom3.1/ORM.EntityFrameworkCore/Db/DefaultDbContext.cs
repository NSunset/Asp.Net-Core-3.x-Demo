using Domain.Entity.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ORM.EntityFrameworkCore.Configuration.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.EntityFrameworkCore.Db
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
