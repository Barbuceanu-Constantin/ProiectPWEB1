using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title).HasMaxLength(50).IsRequired();
        builder.HasAlternateKey(e => e.Title);

        builder.Property(e => e.Sal_min)
            .HasDefaultValue(0)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(e => e.Sal_max)
            .HasDefaultValue(0)
            .HasPrecision(17, 2)
            .IsRequired();

        builder.HasCheckConstraint("CK_Sal_min_NonNegative", "\"Sal_min\" >= 0");
        builder.HasCheckConstraint("CK_Sal_max_NonNegative", "\"Sal_max\" >= 0");
    }
}

