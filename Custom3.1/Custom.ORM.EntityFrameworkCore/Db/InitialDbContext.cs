using Custom.Domain.Entity.Initial;
using Microsoft.EntityFrameworkCore;
using Custom.ORM.EntityFrameworkCore.Configuration.Initial;
using Custom.lib.DbContextConfig;
using Custom.lib.Appsettings;

namespace Custom.ORM.EntityFrameworkCore.Db
{
    public class InitialDbContext : CustomDbContextBase<InitialDbContext>
    {
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }



        private readonly string _connectionStr;
        public InitialDbContext(
            CustomDbContextOptions<InitialDbContext> options,
            IDbContextConfiguration dbContextConfiguration,
            AppSetting appSetting) : base(options, dbContextConfiguration)
        {
            _connectionStr = appSetting.ConnectionStrings.Initial;
        }
       

        protected override string ConnectionStr => _connectionStr;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOrderConfiguration());
        }

    }
}
