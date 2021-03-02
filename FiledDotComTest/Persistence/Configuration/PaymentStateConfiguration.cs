using FiledDotComTest.Domain.Entities;
using FiledDotComTest.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cooperative.Persistence.Configuration
{
    public class UserEntityConfiguration : BaseEntityConfiguration<PaymentState>
    {
        public override void Configure(EntityTypeBuilder<PaymentState> builder)
        {
            builder.Property(t => t.State)
                .HasConversion(v => v.ToString(), v => (Status)Enum.Parse(typeof(Status), v));
            builder.Navigation(d => d.Payment).UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}