using Custom.Domain.Entity.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.ORM.EntityFrameworkCore.Configuration.Default
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Pwd)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
