using Custom.lib.Appsettings;
using Microsoft.Extensions.DependencyInjection;
using Custom.ORM.EntityFrameworkCore.Db;
using Custom.lib.DbContextConfig;

namespace Custom.ORM.EntityFrameworkCore
{
    public static class ContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, Connectionstrings connectionString)
        {
            services.AddDbContext<DefaultDbContext>((serviceProvider, options) =>
            {
                var dbContextConfiguration = serviceProvider.GetService<IDbContextConfiguration>();
                dbContextConfiguration.Configure<DefaultDbContext>(options, connectionString.Default);
            });

            services.AddDbContext<InitialDbContext>((serviceProvider, options) =>
            {
                var dbContextConfiguration = serviceProvider.GetService<IDbContextConfiguration>();
                dbContextConfiguration.Configure<InitialDbContext>(options, connectionString.Initial);
            });

            return services;
        }
    }
}
