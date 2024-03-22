using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;
public class RaionConfiguration : IEntityTypeConfiguration<Raion>
{
    public void Configure(EntityTypeBuilder<Raion> builder)
    {
        builder.Property(e => e.Id).IsRequired();              
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

        //Foreign_keys  
        builder.HasOne<User>()                              // Define navigation property
               .WithOne()                                   // A raion is associated with one user
               .HasForeignKey<Raion>(r => r.SefRaionId)     // Foreign key property in Raion table
               .IsRequired(false);
        //End of foreign keys
    }
}
