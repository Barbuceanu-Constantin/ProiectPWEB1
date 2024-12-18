﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the User entity, generally the Entity Framework will figure out most of the configuration but,
/// for some specifics such as unique keys, indexes and foreign keys it is better to explicitly specify them.
/// Note that the EntityTypeBuilder implements a Fluent interface, meaning it is a highly declarative interface using method-chaining.
/// </summary>
public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.Property(e => e.Id)     // This specifies which property is configured.
            .IsRequired();              // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);      // Here it is specifies that the property Id is the primary key.

        builder.Property(e => e.Name)
            .HasMaxLength(100)          // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.Property(e => e.CountryOfOrigin)
            .HasMaxLength(50)          // This specifies the maximum length for varchar type in the database.
            .IsRequired();
    }
}
