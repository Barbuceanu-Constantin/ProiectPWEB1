using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(x => x.Id);

        //Foreign_keys
        builder.HasOne(r => r.Cashier)
               .WithMany(c => c.Receipts)
               .HasForeignKey(p => p.CashierId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys
    }
}
