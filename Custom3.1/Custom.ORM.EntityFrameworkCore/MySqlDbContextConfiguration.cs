using Custom.lib.DbContextConfig;
using Custom.lib.IOC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Custom.ORM.EntityFrameworkCore
{
    public class MySqlDbContextConfiguration : IDbContextConfiguration, ITransient
    {
        public void Configure<T>(DbContextOptionsBuilder builder, string connectionString) where T : DbContext
        {
            builder.UseMySql(connectionString);
        }

        public void Configure<T>(DbContextOptionsBuilder builder, DbConnection connection) where T : DbContext
        {
            builder.UseMySql(connection);
        }
    }
}
