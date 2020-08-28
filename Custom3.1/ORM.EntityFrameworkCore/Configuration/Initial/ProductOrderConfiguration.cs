using Domain.Entity.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.EntityFrameworkCore.Configuration.Initial
{
    public class ProductOrderConfiguration : BaseConfiguration<ProductOrder>
    {
        public override void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
