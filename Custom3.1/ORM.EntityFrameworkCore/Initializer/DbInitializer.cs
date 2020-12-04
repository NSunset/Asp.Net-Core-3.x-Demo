using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Default;
using Domain.Entity.Initial;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ORM.EntityFrameworkCore.Db;

namespace ORM.EntityFrameworkCore.Initializer
{
    public class DbInitializer
    {
        private static async Task CreateDefaultDbAsync(DefaultDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.User.Any())
            {
                string pwd = "";
                byte[] arr = new System.Security.Cryptography.SHA256Managed().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes("123456"));
                for (int i = 0; i < arr.Length; i++)
                {
                    pwd += arr[i].ToString("X2");
                }
                context.User.Add(new User
                {
                    CreateTime = DateTime.Now,
                    Pwd = pwd,
                    UserName = "小旋风"
                });
                context.SaveChanges();
            }
        }

        private static async Task CreateInitialDbAsync(InitialDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Product.Any())
            {
                context.Product.Add(new Product
                {
                    CreateTime = DateTime.Now,
                    Name = "牛奶",
                    Price = 999.99M
                });
                context.SaveChanges();
            }
        }

        public static async Task<IHost> CreateDbIfNotExistsAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var defaultDb = services.GetRequiredService<DefaultDbContext>();
                    var initialDb = services.GetRequiredService<InitialDbContext>();
                    await CreateDefaultDbAsync(defaultDb);
                    await CreateInitialDbAsync(initialDb);
                }
                catch (Exception ex)
                {
                    ILogger logger = services.GetRequiredService<ILogger<DbInitializer>>();
                    logger.LogError("初始化数据库报错：" + ex);
                }
                return host;
            }
        }
    }
}
