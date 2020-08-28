using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ORM.EntityFrameworkCore.Db;
using System;
using System.Collections.Generic;
using System.Text;
using Common.AppSettings;
using Common.EntityFrameworkCore;

namespace ORM.EntityFrameworkCore
{
    public static class ContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, ConnectionString connectionString)
        {
            IDbContextConfiguration dbContextConfiguration = new MySqlDbContextConfiguration();
            services.AddDbContext<DefaultDbContext>(options =>
            {
                dbContextConfiguration.Configure<DefaultDbContext>(options, connectionString.Default);
                //options.UseMySql(connectionString.Default);
            });

            services.AddDbContext<InitialDbContext>(options =>
            {
                dbContextConfiguration.Configure<InitialDbContext>(options, connectionString.Initial);
                //options.UseMySql(connectionString.Initial);
            });

            return services;
        }
    }
}
