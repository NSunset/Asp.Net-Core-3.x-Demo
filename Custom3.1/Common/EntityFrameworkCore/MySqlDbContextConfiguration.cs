using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Common.EntityFrameworkCore
{
    public class MySqlDbContextConfiguration : IDbContextConfiguration
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
