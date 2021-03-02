using FiledDotComTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cooperative.Persistence.Configuration
{
    public class PaymentEntityConfiguration : BaseEntityConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            
            builder.Property(t => t.CreditCardNumber)
                .IsRequired();
            builder.Property(t => t.CardHolder)
                .IsRequired();
            builder.Property(t => t.ExpirationDate)
                .IsRequired();
            builder.Property(t => t.SecurityCode)
                .HasMaxLength(50)
                .HasMaxLength(50);
            builder.Property<decimal>(t => t.Amount)
                .HasPrecision(18, 2)
                .HasDefaultValue<decimal>(0.00)
                .IsRequired();
            builder.HasOne(d => d.PaymentState)
                .WithOne()
                .HasForeignKey<Payment>(x => x.PaymentStateId);
        }
    }
}