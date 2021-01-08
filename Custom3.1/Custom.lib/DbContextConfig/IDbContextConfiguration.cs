using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Custom.lib.DbContextConfig
{
    public interface IDbContextConfiguration
    {

        void Configure<T>(DbContextOptionsBuilder builder, string connectionString) where T : DbContext;

        void Configure<T>(DbContextOptionsBuilder builder, DbConnection connection) where T : DbContext;
    }
}
