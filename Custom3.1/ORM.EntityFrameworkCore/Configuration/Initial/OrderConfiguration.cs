using Domain.Entity.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.EntityFrameworkCore.Configuration.Initial
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.OrderPlaced).HasDefaultValue();
            builder.Property(x => x.OrderFulfilled).HasDefaultValue();
            builder.Property(x => x.CustomerId).IsRequired();

        }
    }
}
