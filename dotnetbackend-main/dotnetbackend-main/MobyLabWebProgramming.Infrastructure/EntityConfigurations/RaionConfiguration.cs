using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using MobyLabWebProgramming.Core.Entities;
using System.Reflection.Emit;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;
public class RaionConfiguration : IEntityTypeConfiguration<Raion>
{
    public void Configure(EntityTypeBuilder<Raion> builder)
    {
        builder.Property(e => e.Id).IsRequired();              
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique(); //Added. This way it works to be modified not like HasAlternateKey.

        //Foreign_keys  
        builder.HasOne(r => r.User)
               .WithMany(u => u.Raioane)
               .HasForeignKey(r => r.SefRaionId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys
    }
}
