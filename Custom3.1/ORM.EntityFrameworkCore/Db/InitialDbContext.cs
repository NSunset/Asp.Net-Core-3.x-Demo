using Domain.Entity.Initial;
using Microsoft.EntityFrameworkCore;
using ORM.EntityFrameworkCore.Configuration.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.EntityFrameworkCore.Db
{
    public class InitialDbContext : DbContext
    {
        public InitialDbContext(DbContextOptions<InitialDbContext> options) : base(options)
        {

        }


        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }


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
