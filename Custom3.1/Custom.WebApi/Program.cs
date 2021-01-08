using System;
using Custom.lib.HostingExtensions;
using Custom.lib.LogUtil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Custom.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                IHost host = CreateHostBuilder(args).Build();
                host.CreateDbIfNotExists();
                host.Run();
            }
            catch (Exception ex)
            {
                LogHelp.Error("³ÌÐòÆô¶¯Ê±´íÎó:" + ex.Message, ex);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseAutoFacServiceProviderToFactory()
                .ConfigureLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
