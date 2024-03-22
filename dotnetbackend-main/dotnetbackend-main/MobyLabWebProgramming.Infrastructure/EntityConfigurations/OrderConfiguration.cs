using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(x => x.Id);

        //Foreign_keys
        builder.HasOne(c => c.Client)
               .WithMany(u => u.Orders)
               .HasForeignKey(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys
    }
}
