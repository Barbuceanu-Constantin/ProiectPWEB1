﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        //Foreign_keys
        builder.HasOne(t => t.Product)
               .WithMany(p => p.Transactions)
               .HasForeignKey(t => t.ProductId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(t => t.Receipt)
               .WithMany(r => r.Transactions)
               .HasForeignKey(t => t.ReceiptId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        //End of foreign keys
    }
}