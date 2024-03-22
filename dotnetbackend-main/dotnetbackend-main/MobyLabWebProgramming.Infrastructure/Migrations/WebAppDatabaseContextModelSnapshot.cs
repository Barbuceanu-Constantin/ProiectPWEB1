﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobyLabWebProgramming.Infrastructure.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    [DbContext(typeof(WebAppDatabaseContext))]
    partial class WebAppDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "unaccent");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("Sal_max")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(17, 2)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<float>("Sal_min")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(5, 2)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Job");

                    b.HasCheckConstraint("CK_Sal_max_NonNegative", "\"Sal_max\" >= 0");

                    b.HasCheckConstraint("CK_Sal_min_NonNegative", "\"Sal_min\" >= 0");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.JoinProviderRaion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RaionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("RaionId");

                    b.ToTable("JoinProviderRaion");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<float>("Price")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(12, 2)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Warranty")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Product");

                    b.HasCheckConstraint("CK_Pricce_NonNegative", "\"Price\" >= 0");

                    b.HasCheckConstraint("CK_Quantity_NonNegative", "\"Quantity\" >= 0");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CountryOfOrigin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Raion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("SefRaionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SefRaionId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Raion");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Commission")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("HireDate")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<float>("Salary")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(12, 2)
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("JobId");

                    b.HasIndex("ManagerId");

                    b.ToTable("User");

                    b.HasCheckConstraint("CK_Commission_NonNegative", "\"Commission\" >= 0");

                    b.HasCheckConstraint("CK_Salary_NonNegative", "\"Salary\" >= 0");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.UserFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(4095)
                        .HasColumnType("character varying(4095)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFile");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.JoinProviderRaion", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.Provider", "Provider")
                        .WithMany("Raioane")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobyLabWebProgramming.Core.Entities.Raion", "Raion")
                        .WithMany("Providers")
                        .HasForeignKey("RaionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("Raion");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Order", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.User", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Product", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MobyLabWebProgramming.Core.Entities.Provider", "Provider")
                        .WithMany("Products")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Raion", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("MobyLabWebProgramming.Core.Entities.Raion", "SefRaionId");

                    b.HasOne("MobyLabWebProgramming.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.User", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.Job", "Job")
                        .WithMany("Users")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MobyLabWebProgramming.Core.Entities.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Job");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.UserFile", b =>
                {
                    b.HasOne("MobyLabWebProgramming.Core.Entities.User", "User")
                        .WithMany("UserFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Job", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Provider", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Raioane");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.Raion", b =>
                {
                    b.Navigation("Providers");
                });

            modelBuilder.Entity("MobyLabWebProgramming.Core.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
