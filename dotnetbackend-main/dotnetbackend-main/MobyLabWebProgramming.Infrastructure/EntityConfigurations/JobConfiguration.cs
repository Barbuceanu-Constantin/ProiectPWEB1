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

        builder.Property(e => e.Sal_min)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(e => e.Sal_max)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.HasCheckConstraint("CK_Sal_min_NonNegative", "Sal_min >= 0");
        builder.HasCheckConstraint("CK_Sal_max_NonNegative", "Sal_max >= 0");
    }
}

