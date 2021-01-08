using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Custom.lib.DbContextConfig;

namespace Custom.lib.HostingExtensions
{
    public static class CustomHostingHostBuilderExtensions
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging((context, builder) =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddLog4Net();
            });
            return hostBuilder;
        }

        public static IHostBuilder UseAutoFacServiceProviderToFactory(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            return hostBuilder;
        }

        public static IHost CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var dbContextSeedData = scope.ServiceProvider.GetService<IDbContextSeedData>();
            dbContextSeedData.Initializer();
            return host;
        }
    }
}
