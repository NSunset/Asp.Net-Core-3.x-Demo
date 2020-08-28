using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.EntityFrameworkCore.Configuration
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : GeneralEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateTime).IsRequired();
        }
    }
}
