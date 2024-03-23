using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class JoinProviderRaionConfiguration : IEntityTypeConfiguration<JoinProviderRaion>
{
    public void Configure(EntityTypeBuilder<JoinProviderRaion> builder)
    {
        builder.Property(e => e.Id)     // This specifies which property is configured.
            .IsRequired();              // Here it is specified if the property is required, meaning it cannot be null in the database.

        //Foreign_keys
        builder.HasOne(j => j.Provider)
               .WithMany(p => p.Raioane)
               .HasForeignKey(p => p.ProviderId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(j => j.Raion)
               .WithMany(p => p.Providers)
               .HasForeignKey(c => c.RaionId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys
    }
}
