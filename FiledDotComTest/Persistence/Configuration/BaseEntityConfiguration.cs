using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cooperative.Persistence.Configuration
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");
            builder.Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property<DateTime>("CreatedDate")
                .IsRequired()
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
            builder.Property<DateTime>("UpdatedDate")
                .IsRequired()
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
