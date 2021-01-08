using Custom.Domain.Entity.Default;
using Custom.Domain.Entity.Initial;
using Custom.lib.DbContextConfig;
using Custom.lib.IOC;
using Custom.lib.LogUtil;
using Custom.ORM.EntityFrameworkCore.Db;
using System;
using System.Linq;

namespace Custom.ORM.EntityFrameworkCore.Initializer
{
    public class CustomInitializer : IDbContextSeedData, ISingleton
    {
        private readonly DefaultDbContext _defaultDbContext;
        private readonly InitialDbContext _initialDbContext;
        public CustomInitializer(DefaultDbContext defaultDbContext, InitialDbContext initialDbContext)
        {
            _defaultDbContext = defaultDbContext;
            _initialDbContext = initialDbContext;
        }

        public void Initializer()
        {
            try
            {
                CreateDefaultDb(_defaultDbContext);
                CreateInitialDb(_initialDbContext);
            }
            catch (Exception ex)
            {
                LogHelp.Error("初始化数据库报错:" + ex.Message, ex);
            }
        }

        private void CreateDefaultDb(DefaultDbContext context)
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

        private void CreateInitialDb(InitialDbContext context)
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
    }
}
