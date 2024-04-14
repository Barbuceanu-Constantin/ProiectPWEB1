using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(e => e.Id)     // This specifies which property is configured.
            .IsRequired();              // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);      // Here it is specifies that the property Id is the primary key.

        builder.Property(e => e.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired(false);

        //Foreign keys
        builder.HasOne(p => p.Order)
               .WithOne(o => o.Payment)
               .HasForeignKey<Payment>(p => p.OrderId)  // Foreign key property in Payment
               .IsRequired();                           // Mandatory foreign key
    }
}
