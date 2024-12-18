﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the User entity, generally the Entity Framework will figure out most of the configuration but,
/// for some specifics such as unique keys, indexes and foreign keys it is better to explicitly specify them.
/// Note that the EntityTypeBuilder implements a Fluent interface, meaning it is a highly declarative interface using method-chaining.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Id)     // This specifies which property is configured.
            .IsRequired();              // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);      // Here it is specifies that the property Id is the primary key.

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(e => e.Name).IsUnique(); //Added. This way it works to be modified not like HasAlternateKey.

        builder.Property(e => e.Description)
            .HasMaxLength(50)
            .IsRequired(false);
        builder.Property(e => e.Warranty)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(e => e.Price)
            .HasDefaultValue(0)
            .HasPrecision(12, 2)
            .IsRequired();
        builder.Property(e => e.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        //Foreign_keys
        builder.HasOne(p => p.Raion)
               .WithMany(r => r.Products)
               .HasForeignKey(p => p.RaionId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(p => p.Provider) 
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.ProviderId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys

        builder.HasCheckConstraint("CK_Pricce_NonNegative", "\"Price\" >= 0");
        builder.HasCheckConstraint("CK_Quantity_NonNegative", "\"Quantity\" >= 0");
    }
}
