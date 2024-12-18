﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the User entity, generally the Entity Framework will figure out most of the configuration but,
/// for some specifics such as unique keys, indexes and foreign keys it is better to explicitly specify them.
/// Note that the EntityTypeBuilder implements a Fluent interface, meaning it is a highly declarative interface using method-chaining.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id)     // This specifies which property is configured.
            .IsRequired();              // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);      // Here it is specifies that the property Id is the primary key.
        
        builder.Property(e => e.Name)
            .HasMaxLength(255)          // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.HasIndex(e => e.Name).IsUnique(); //Added. This way it works to be modified not like HasAlternateKey.

        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasAlternateKey(e => e.Email); // Here it is specifies that the property Email is a unique key.
        builder.Property(e => e.Password)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(e => e.HireDate)
            .HasMaxLength(50)
            .IsRequired(false);
        builder.Property(e => e.Salary)
            .HasDefaultValue(0)
            .HasPrecision(12, 2)
            .IsRequired();
        builder.Property(e => e.Commission)
            .HasDefaultValue(0)
            .HasPrecision(7, 2)
            .IsRequired();

        //Foreign_keys
        builder.HasOne(u => u.Job)              // Define navigation property
               .WithMany(j => j.Users)          // A job can be assigned to multiple users
               .HasPrincipalKey(j => j.Title)   // Functia asta permite legarea la Unique 
               .HasForeignKey(u => u.JobTitle)     // Foreign keyproperty
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
        //End of foreign keys

        builder.Property(e => e.Role)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasCheckConstraint("CK_Salary_NonNegative", "\"Salary\" >= 0");
        builder.HasCheckConstraint("CK_Commission_NonNegative", "\"Commission\" >= 0");
    }
}
